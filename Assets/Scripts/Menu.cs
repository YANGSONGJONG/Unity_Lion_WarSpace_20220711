using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// �ޥ�UI�{���w
using UnityEngine.UI;
// ���\Ū���P�g�J�ɮשθ��
using System.IO;
using UnityEngine.Audio;

namespace NRSUNG
{
    /// <summary>
    /// ���{��
    /// </summary>
    public class Menu : MonoBehaviour
    {
        [Header("BGM")]
        public GameObject BGM;

        // ��������n���}��
        bool ControlAudio;
/*��k�@
        [Header("�n���}�Ϥ�")]
        public Sprite OpenSound;
        [Header("�n�����Ϥ�")]
        public Sprite CloseSound; 
*/
        [Header("�n�����s")]
        public Image ButtonSound;

        // �ȦsSstreamingAssets���|
        public string[] filePaths;

        [Header("�վ㭵�qSlider")]
        public Slider ChangeAudioSlider;

        [Header("AudioMixer")]
        public AudioMixer AudioMixerObj;

        [Header("�ѪR��Dropdown")]
        public Dropdown SizeDropdown;

        [Header("�y��Dropdown")]
        public Dropdown LanDropdown;
        //�Ȧs�y��Dropdown��ID��
        string SaveLanID = "SaveLanID";
        public InputField[] Keyboards;

        private void Awake()
        {
            // �^��StreamingAssets��Ƨ����ɮ׮榡��png���Ҧ����ɸ��|
            filePaths = Directory.GetFiles(Application.streamingAssetsPath, "*.png");

#if UNITY_STANDALONE_WIN
            SizeDropdown.interactable = true;
#endif
#if UNITY_ANDROID || UNITY_IOS
            SizeDropdown.interactable = false;
#endif
            Debug.Log(Staticvar.KeyboardsState[0]);
            
            if (Staticvar.KeyboardsState[0] == null || Staticvar.KeyboardsState[2] == null || Staticvar.KeyboardsState[3] == null)
            {
                Keyboards[0].text = "w";
                Keyboards[1].text = "s";
                Keyboards[2].text = "a";
                Keyboards[3].text = "d";
                for (int i = 0; i < Keyboards.Length; i++)
                    Staticvar.KeyboardsState[i] = Keyboards[i].text;
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            // �ˬd�����WBGM�ƶq�O�_<=0
            if (GameObject.FindGameObjectsWithTag("BGM").Length <= 0)
            {
                // �ʺA�ͦ��@�ӭI�����֪���
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

#region ��������n��
        public void Cintrol_Audio()
        {
            ControlAudio = !ControlAudio;
            if (ControlAudio)
            {
                //ButtonSound.sprite = OpenSound;
                //ButtonSound.sprite = Resources.Load<Sprite>("soundON");
                // Ū��OpenAudio����
                StreamingAssetsLoadTexture(0);
            }
            else
            {
                //ButtonSound.sprite = CloseSound;
                //ButtonSound.sprite = Resources.Load<Sprite>("soundOFF");
                // Ū��CloseAudio����
                StreamingAssetsLoadTexture(1);
            }
            // AudioListener.pause = true ; ���������n���R��
            // AudioListener.pause = False ; ���������n���}��
            AudioListener.pause = ControlAudio;
        }
#endregion

        void StreamingAssetsLoadTexture(int ID)
        {
            // �N���|����ഫ��2�i���ɮ�
            byte[] pngBytes = File.ReadAllBytes(filePaths[ID]);
            // �ŧi2D�Ϥ�(�Ϥ��e,�Ϥ���)
            Texture2D tex = new Texture2D(0, 0);
            // Ū���Ϥ�
            tex.LoadImage(pngBytes);
            // �N�Ϥ��ഫ��Sprite�榡(�Ϥ�,�x��(��m.x,��m.y,�e��,����),Pivot�����I��m(x,y))
            Sprite FormTex = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            // �a�J�Ϥ���Button��Image��
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
            //�x�s����]�w 0=�W,1=�U,2=��,3=�k
           public void SetKeyboard(int ID)
        {
            Staticvar.KeyboardsState[ID] = Keyboards[ID].text;
        }
        
    }
}


