using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float MinX, MaxX, MinY, MaxY;
    bool isTouch;
    public GameObject Bullet;
    [Header("設定多少時間產生一個子彈")]
    public float SetTime;
    float ScriptTime;
    [Header("參考位置")]
    public Transform TargetPoint;
    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(0, 1, 0);
        }*/
        /*#if UNITY_STANDALONE_WIN
                transform.Translate(Input.GetAxis("Horizontal")* Speed*Time.deltaTime, 0, Input.GetAxis("Vertical") * Speed * Time.deltaTime);
        #endif
        #if UNUTY_ANDROID
                transform.Translate(Input.acceleration.x* Speed*Time.deltaTime, 0, Input.acceleration.y* Speed * Time.deltaTime);
        #endif*/
        if (isTouch)
        {
            //Camera.main.ScreenToWorldPoint(遊戲視窗的滑鼠或手指的Vector2); 遊戲場景的滑鼠或手指的Vector2轉換成Unity 編輯環境中的3維數值
            //Camera.main.WorldToScreenPoint(3D物件的座標位置) Unity 編輯環境中的3維數值 轉換成 遊戲場景的滑鼠或手指的Vector2值
            //Input.mousePosition 滑鼠左鍵
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -1f);
        
        }
        //Debug.Log(Input.mousePosition);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinX, MaxX), Mathf.Clamp(transform.position.y, MinY, MaxY), transform.position.z);
        ScriptTime += Time.deltaTime;
        if (ScriptTime >= SetTime)
        {
            Instantiate(Bullet, TargetPoint.transform.position, TargetPoint.transform.rotation);
            ScriptTime = 0;
        }
    }
    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown");
        isTouch = true;
    }
 
    private void OnMouseUp()
    {
        isTouch = false;
    }
}
