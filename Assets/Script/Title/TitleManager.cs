using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // Gマネージャー
    public GameManager gMana;

    // メニュー画面のオブジェクトまとめ
    public GameObject menuBox;

    // メニュー画面表示フラグ(GMで使うからpublic)
    public bool isMenuFlag = false;

    // サイコロの絵
    public Image dice;
    bool isSelect = true;

    // コントローラのスティック受け入れ
    float Vertical;

    // 設定用スライダー
    public Slider sliderBgm;
    public Image handBgm;
    public Slider sliderSe;
    public Image handSe;

    // タイトルの文字
    public Image startText;
    public Image configText;

    // 大きくなってるか
    bool isBig = false;

    // Start is called before the first frame update
    void Start()
    {
        sliderBgm.value = PlayerPrefs.GetFloat("BGMSlider");
        sliderSe.value = PlayerPrefs.GetFloat("SESlider");
        dice.transform.localPosition = new Vector3(-514.0f, -178.0f, -8.0f);
        startText.transform.localScale += new Vector3(0.5f, 0.2f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // コンフィグ中ではスティックを受け付けない
        if (!isMenuFlag)
        {
            Vertical = Input.GetAxisRaw("L_Stick_V");
        }

        // 選択移動
        if (Vertical == -1 && isSelect)
        {
            // 選択しているフォントがでかくなっているか
            isBig = true;
            // コンフィグ選択
            isSelect = false;
            // 選択アイコンのサイコロの位置
            dice.transform.localPosition = new Vector3(-363.0f, -347.0f, -8.0f);
            // 選択アイコンのサイコロの回転
            dice.transform.Rotate(0, 0, 90);
        }
        else if (Vertical == 1 && !isSelect)
        {
            // 選択しているフォントがでかくなっているか
            isBig = true;
            // スタート選択
            isSelect = true;
            // 選択アイコンのサイコロの位置
            dice.transform.localPosition = new Vector3(-514.0f, -178.0f, -8.0f);
            // 選択アイコンのサイコロの回転
            dice.transform.Rotate(0, 0, 90);
        }

        // 選択しているフォント拡大
        if (isSelect && isBig)
        {
            isBig = false;
            startText.transform.localScale += new Vector3(0.5f, 0.2f, 0.0f);
            configText.transform.localScale -= new Vector3(0.5f, 0.2f, 0.0f);
        }
        else if (isBig && !isSelect)
        {
            isBig = false;
            configText.transform.localScale += new Vector3(0.5f, 0.2f, 0.0f);
            startText.transform.localScale -= new Vector3(0.5f, 0.2f, 0.0f);
        }

        // シーンPlayへ移動
        if ((Input.GetKeyDown("joystick button 0")) && !isMenuFlag && isSelect)
        {
            gMana.ChangeScene2("Play");
        }

        // 設定画面の表示
        if ((Input.GetKeyDown("joystick button 0")) && !isMenuFlag && !isSelect)
        {
            isMenuFlag = true;
        }
        else if ((Input.GetKeyDown("joystick button 0")) && isMenuFlag && !isSelect)
        {
            isMenuFlag = false;
        }

        if(Input.GetKey(KeyCode.B))
        {
            isMenuFlag = true;
        }

        // メニュー画面関係
        CloseConfig();
        if (isMenuFlag)
        {
            OpenConfig();

            // BGM上げ下げ
            if (Input.GetKey("joystick button 5"))
            {
                UpBgm();
            }

            if (Input.GetKey("joystick button 4"))
            {
                DownBgm();
            }

            // LT,RTの入力(InputManagerにある)
            float tri = Input.GetAxis("L_R_Trigger");
            // SE上げさげ
            if (tri > 0)
            {
                UpSe();
            }
            if (tri < 0)
            {
                DownSe();
            }
        }
    }

    // 設定画面を開く
    void OpenConfig()
    {
        menuBox.SetActive(true);
    }

    // 設定画面を閉じる
    void CloseConfig()
    {
        menuBox.SetActive(false);
    }

    // 音量上げる
    public void UpBgm()
    {
        sliderBgm.value += 0.01f;
        gMana.BGM[0].volume = sliderBgm.value;
        PlayerPrefs.SetFloat("BGMVolume", gMana.BGM[0].volume);
        PlayerPrefs.SetFloat("BGMSlider", sliderBgm.value);
        if (sliderBgm.value < 1)
        {
            handBgm.transform.Rotate(0.0f, 0.0f, -3.5f);
        }
    }

    // 音量下げる
    public void DownBgm()
    {
        sliderBgm.value -= 0.01f;
        gMana.BGM[0].volume = sliderBgm.value;
        PlayerPrefs.SetFloat("BGMVolume", gMana.BGM[0].volume);
        PlayerPrefs.SetFloat("BGMSlider", sliderBgm.value);
        if (sliderBgm.value > 0)
        {
            handBgm.transform.Rotate(0.0f, 0.0f, 3.5f);
        }
    }

    // SEを上げる
    public void UpSe()
    {
        sliderSe.value += 0.01f;
        gMana.SE[0].volume = sliderSe.value;
        PlayerPrefs.SetFloat("SEVolume", gMana.SE[0].volume);
        PlayerPrefs.SetFloat("SESlider", sliderSe.value);
        if (sliderSe.value < 1)
        {
            handSe.transform.Rotate(0.0f, 0.0f, -3.5f);
        }
    }

    // SEを下げる
    public void DownSe()
    {
        sliderSe.value -= 0.01f;
        gMana.SE[0].volume = sliderSe.value;
        PlayerPrefs.SetFloat("SEVolume", gMana.SE[0].volume);
        PlayerPrefs.SetFloat("SESlider", sliderSe.value);
        if (sliderSe.value > 0)
        {
            handSe.transform.Rotate(0.0f, 0.0f, 3.5f);
        }
    }
}
