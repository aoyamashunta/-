using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prime31.TransitionKit;

public class OverManager : MonoBehaviour
{
    // G�}�l�[�W���[
    public GameManager gMana;

    // �}�X�N�摜
    public Texture2D maskTexture;

    // �^�C�g���̕���
    public Image titleText;
    public Image continueText;

    // �I���A�C�R��
    public Image dice;

    // GameOver�̃e�L�X�g
    public Material nameShader;

    public Plane name;

    // �R���g���[���̃X�e�B�b�N�󂯓���
    float horizontal;

    // �I��
    bool isSelect = true;

    // �I�����Ă���e�L�X�g��傫��
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

            // �I���ړ�
            if (horizontal == 1 && isSelect)
            {
                gMana.SE[1].Play();

                // �I�����Ă���t�H���g���ł����Ȃ��Ă��邩
                isBig = true;
                // �R���t�B�O�I��
                isSelect = false;
                // �I���A�C�R���̃T�C�R���̈ʒu
                dice.transform.localPosition = new Vector3(118.0f, -444.0f, 0f);
                // �I���A�C�R���̃T�C�R���̉�]
                dice.transform.Rotate(0, 0, 90);
            }
            else if (horizontal == -1 && !isSelect)
            {
                gMana.SE[1].Play();

                // �I�����Ă���t�H���g���ł����Ȃ��Ă��邩
                isBig = true;
                // �X�^�[�g�I��
                isSelect = true;
                // �I���A�C�R���̃T�C�R���̈ʒu
                dice.transform.localPosition = new Vector3(-862.0f, -444.0f, 0f);
                // �I���A�C�R���̃T�C�R���̉�]
                dice.transform.Rotate(0, 0, 90);
            }

            // �I�����Ă���t�H���g�g��
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

            // �V�[��Play�ֈړ�
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

            // �f�o�b�N�p
            if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown("joystick button 0") && isSelect)
            {
                gMana.ChangeScene2("Title");
                var squares = new SquaresTransition()
                {
                    //nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
                    // �Ԋu
                    duration = 1.0f,
                    // �傫��
                    squareSize = new Vector2(64f, 45f),
                    // �F
                    squareColor = Color.black,
                    // ���炩��
                    smoothness = 0.6f
                };
                TransitionKit.instance.transitionWithDelegate(squares);
            }
        }
    }
}
