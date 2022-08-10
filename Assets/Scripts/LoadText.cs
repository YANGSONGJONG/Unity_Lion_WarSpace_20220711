using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadText : MonoBehaviour
{
    [Header("輸入每個文字自己的ID")]
    public int ID;

    string SaveLanID = "SaveLanID";

    private void Update()
    {
        switch (PlayerPrefs.GetInt(SaveLanID))
        {
            case 0:
                gameObject.GetComponent<Text>().text = FindObjectOfType<ReadText>().CHDatas[ID];
                break;

            case 1:
                gameObject.GetComponent<Text>().text = FindObjectOfType<ReadText>().ENDatas[ID];
                break;
        }
    }
}
