using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
  
    [Header("固定每幾秒產生一個物件")]
    public float SetTime;
    [Header("敵人")]
    public GameObject[] Enemys;

    [Header("X邊界最大值")]
    public float MaxX;
    [Header("X邊界最小值")]
    public float MinX;

    [Header("PauseUI")]
    public GameObject PauseUI;

    [Header("玩家總血量")]
    public float TotalHP;
    //程式中計算玩家血量
    float ScriptHP;
    [Header("玩家血條")]
    public Image PlayerHPImage;

    [Header("分數文字")]
    public Text ScoreText;
    int TotalScore;
    // Start is called before the first frame update
    void Start()
    {
        ScriptHP = TotalHP;
        InvokeRepeating("CreateEnemys", SetTime, SetTime);
    }

    void CreateEnemys()
    {
        Instantiate(Enemys[Random.Range(0, Enemys.Length)], new Vector3(Random.Range(MinX, MaxX), transform.position.y, transform.position.z), transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
   
    }
    public void Pause(bool isPause)
    {
        //Time.timeScale = 0; 整體時間暫停
        //Time.timeScale = 1; 整體時間復原
        /*if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }*/
        PauseUI.SetActive(isPause ? true : false);
        FindObjectOfType<Player>().enabled = isPause ? false : true;
        Time.timeScale = isPause ? 0 : 1;
    }
    
    public void HurtPlayer(float Hurt)
    {
        ScriptHP -= Hurt;
        PlayerHPImage.fillAmount = ScriptHP / TotalHP;
        if (PlayerHPImage.fillAmount <= 0)
        {
            Application.LoadLevel("GameOver");
        }
    }
    public void AddScore(int Add)
    {
        TotalScore += Add;
        ScoreText.text = "Score : " + TotalScore;
    }
}
