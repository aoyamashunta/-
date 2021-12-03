using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverManager : MonoBehaviour
{
    // Gマネージャー
    public GameManager gMana;

    // タイトルの文字
    public Image titleText;
    public Image continueText;

    public Image dice;

    // コントローラのスティック受け入れ
    float horizontal;

    bool isSelect = true;

    bool isBig = false;

    // Start is called before the first frame update
    void Start()
    {
        dice.transform.localPosition = new Vector3(-862.0f, -444.0f, 0f);
        titleText.transform.localScale += new Vector3(0.5f, 0.2f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("L_Stick_H");
        

        // 選択移動
        if (horizontal == 1 && isSelect)
        {
            // 選択しているフォントがでかくなっているか
            isBig = true;
            // コンフィグ選択
            isSelect = false;
            // 選択アイコンのサイコロの位置
            dice.transform.localPosition = new Vector3(118.0f, -444.0f, 0f);
            // 選択アイコンのサイコロの回転
            dice.transform.Rotate(0, 0, 90);
        }
        else if (horizontal == -1 && !isSelect)
        {
            // 選択しているフォントがでかくなっているか
            isBig = true;
            // スタート選択
            isSelect = true;
            // 選択アイコンのサイコロの位置
            dice.transform.localPosition = new Vector3(-862.0f, -444.0f, 0f);
            // 選択アイコンのサイコロの回転
            dice.transform.Rotate(0, 0, 90);
        }

        // 選択しているフォント拡大
        if (isSelect && isBig)
        {
            isBig = false;
            titleText.transform.localScale += new Vector3(0.5f, 0.2f, 0.0f);
            continueText.transform.localScale -= new Vector3(0.5f, 0.2f, 0.0f);
        }
        else if (isBig && !isSelect)
        {
            isBig = false;
            continueText.transform.localScale += new Vector3(0.5f, 0.2f, 0.0f);
            titleText.transform.localScale -= new Vector3(0.5f, 0.2f, 0.0f);
        }


        // シーンPlayへ移動
        if ((Input.GetKeyDown("joystick button 0")) && !isSelect)
        {
            gMana.ChangeScene2("Play");
        }

        if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown("joystick button 0") && isSelect)
        {
            gMana.ChangeScene2("Title");
        }
    }
}
