using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverManager : MonoBehaviour
{
    // G�}�l�[�W���[
    public GameManager gMana;

    // �^�C�g���̕���
    public Image titleText;
    public Image continueText;

    public Image dice;

    // �R���g���[���̃X�e�B�b�N�󂯓���
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
        

        // �I���ړ�
        if (horizontal == 1 && isSelect)
        {
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
        }

        if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown("joystick button 0") && isSelect)
        {
            gMana.ChangeScene2("Title");
        }
    }
}
