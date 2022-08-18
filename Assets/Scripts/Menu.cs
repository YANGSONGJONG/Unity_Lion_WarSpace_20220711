using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// 引用UI程式庫
using UnityEngine.UI;
// 允許讀取與寫入檔案或資料
using System.IO;
using UnityEngine.Audio;

namespace NRSUNG
{
    /// <summary>
    /// 選單程式
    /// </summary>
    public class Menu : MonoBehaviour
    {
        [Header("BGM")]
        public GameObject BGM;

        // 控制整體聲音開關
        bool ControlAudio;
/*方法一
        [Header("聲音開圖片")]
        public Sprite OpenSound;
        [Header("聲音關圖片")]
        public Sprite CloseSound; 
*/
        [Header("聲音按鈕")]
        public Image ButtonSound;

        // 暫存SstreamingAssets路徑
        public string[] filePaths;

        [Header("調整音量Slider")]
        public Slider ChangeAudioSlider;

        [Header("AudioMixer")]
        public AudioMixer AudioMixerObj;

        [Header("解析度Dropdown")]
        public Dropdown SizeDropdown;

        [Header("語言Dropdown")]
        public Dropdown LanDropdown;
        //暫存語言Dropdown的ID值
        string SaveLanID = "SaveLanID";
        public InputField[] Keyboards;

        private void Awake()
        {
            // 回傳StreamingAssets資料夾內檔案格式為png的所有圖檔路徑
            filePaths = Directory.GetFiles(Application.streamingAssetsPath, "*.png");

#if UNITY_STANDALONE_WIN
            SizeDropdown.interactable = true;
#endif
#if UNITY_ANDROID || UNITY_IOS
            SizeDropdown.interactable = false;
#endif
            Debug.Log(Staticvar.KeyboardsState[0]);
            
            /*if (Staticvar.KeyboardsState[0] == null || Staticvar.KeyboardsState[2] == null || Staticvar.KeyboardsState[3] == null)
            {
                Keyboards[0].text = "w";
                Keyboards[1].text = "s";
                Keyboards[2].text = "a";
                Keyboards[3].text = "d";
                for (int i = 0; i < Keyboards.Length; i++)
                    Staticvar.KeyboardsState[i] = Keyboards[i].text;
            }*/
        }


        // Start is called before the first frame update
        void Start()
        {
            // 檢查場景上BGM數量是否<=0
            if (GameObject.FindGameObjectsWithTag("BGM").Length <= 0)
            {
                // 動態生成一個背景音樂物件
                Instantiate(BGM);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NextScene()
        {
            SceneManager.LoadScene("Level");
        }

        public void Quit()
        {
            Application.Quit();
        }

#region 控制整體聲音
        public void Cintrol_Audio()
        {
            ControlAudio = !ControlAudio;
            if (ControlAudio)
            {
                //ButtonSound.sprite = OpenSound;
                //ButtonSound.sprite = Resources.Load<Sprite>("soundON");
                // 讀取OpenAudio圖檔
                StreamingAssetsLoadTexture(0);
            }
            else
            {
                //ButtonSound.sprite = CloseSound;
                //ButtonSound.sprite = Resources.Load<Sprite>("soundOFF");
                // 讀取CloseAudio圖檔
                StreamingAssetsLoadTexture(1);
            }
            // AudioListener.pause = true ; 整體環境聲音靜音
            // AudioListener.pause = False ; 整體環境聲音開啟
            AudioListener.pause = ControlAudio;
        }
#endregion

        void StreamingAssetsLoadTexture(int ID)
        {
            // 將路徑資料轉換成2進位檔案
            byte[] pngBytes = File.ReadAllBytes(filePaths[ID]);
            // 宣告2D圖片(圖片寬,圖片高)
            Texture2D tex = new Texture2D(0, 0);
            // 讀取圖片
            tex.LoadImage(pngBytes);
            // 將圖片轉換成Sprite格式(圖片,矩形(位置.x,位置.y,寬度,高度),Pivot中心點位置(x,y))
            Sprite FormTex = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            // 帶入圖片到Button的Image中
            ButtonSound.sprite = FormTex;
        }

        public void ChangeAudio_Slider()
        {
            //AudioListener.volume = ChangeAudioSlider.value;
            AudioMixerObj.SetFloat("BGM", ChangeAudioSlider.value);
        }

        public void ChangeScreenSize()
        {
            switch (SizeDropdown.value)
            {
                case 0:
                    Screen.SetResolution(1080, 1920, false);
                    break;
                case 1:
                    Screen.SetResolution(720, 1280, false);
                    break;
                case 2:
                    Screen.SetResolution(480, 800, false);
                    break;
            }

        }

            public void ChangeLan()
            {
                PlayerPrefs.SetInt(SaveLanID, LanDropdown.value);
            }
            //儲存按鍵設定 0=上,1=下,2=左,3=右
           public void SetKeyboard(int ID)
        {
            Staticvar.KeyboardsState[ID] = Keyboards[ID].text;
        }
        
    }
}


