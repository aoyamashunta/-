using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // Gマネージャー
    public GameManager gMana;

    public Image menu;
    public Image menuBack;

    public Slider sliderSound;
    public Slider sliderLight;

    public bool isMenuFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        menu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 設定画面の表示
        if(Input.GetKeyDown(KeyCode.Q) && !isMenuFlag)
        {
            isMenuFlag = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q)&& isMenuFlag)
        {
            isMenuFlag = false;
        }

        CloseConfig();
        if (isMenuFlag)
        {
            OpenConfig();
        }
    }

    // 設定画面を開く
    void OpenConfig()
    {
        menu.enabled = true;
        menuBack.enabled = true;
        sliderLight.gameObject.SetActive(true);
        sliderSound.gameObject.SetActive(true);
      
    }

    // 設定画面を閉じる
    void CloseConfig()
    {
        menu.enabled = false;
        menuBack.enabled = false;
        sliderLight.gameObject.SetActive(false);
        sliderSound.gameObject.SetActive(false);
    }

}
