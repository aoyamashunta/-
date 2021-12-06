using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prime31.TransitionKit;

public class OverManager : MonoBehaviour
{
    // Gマネージャー
    public GameManager gMana;

    // マスク画像
    public Texture2D maskTexture;

    // タイトルの文字
    public Image titleText;
    public Image continueText;

    // 選択アイコン
    public Image dice;

    // GameOverのテキスト
    public Material nameShader;

    public Plane name;

    // コントローラのスティック受け入れ
    float horizontal;

    // 選択
    bool isSelect = true;

    // 選択しているテキストを大きく
    bool isBig = false;

    float diss = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        dice.transform.localPosition = new Vector3(-862.0f, -444.0f, 0f);
        titleText.transform.localScale += new Vector3(0.5f, 0.2f, 0.0f);

        nameShader.SetFloat("_Dissolve", 1.1f);

        titleText.enabled = false;
        continueText.enabled = false;
        dice.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (nameShader.GetFloat("_Dissolve") >= 0.15f)
        {
            diss -= 0.01f;
            nameShader.SetFloat("_Dissolve", diss);
        }



        if (nameShader.GetFloat("_Dissolve") <= 0.15f)
        {
            titleText.enabled = true;
            continueText.enabled = true;
            dice.enabled = true;

            horizontal = Input.GetAxisRaw("L_Stick_H");

            // 選択移動
            if (horizontal == 1 && isSelect)
            {
                gMana.SE[1].Play();

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
                gMana.SE[1].Play();

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
                var mask = new ImageMaskTransition()
                {
                    maskTexture = maskTexture,
                    backgroundColor = Color.red,
                };
                TransitionKit.instance.transitionWithDelegate(mask);
            }

            // デバック用
            if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown("joystick button 0") && isSelect)
            {
                gMana.ChangeScene2("Title");
                var squares = new SquaresTransition()
                {
                    //nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
                    // 間隔
                    duration = 1.0f,
                    // 大きさ
                    squareSize = new Vector2(64f, 45f),
                    // 色
                    squareColor = Color.black,
                    // 滑らかさ
                    smoothness = 0.6f
                };
                TransitionKit.instance.transitionWithDelegate(squares);
            }
        }
    }
}
