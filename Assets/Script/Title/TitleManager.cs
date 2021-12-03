using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // G�}�l�[�W���[
    public GameManager gMana;

    // ���j���[��ʂ̃I�u�W�F�N�g�܂Ƃ�
    public GameObject menuBox;

    // ���j���[��ʕ\���t���O(GM�Ŏg������public)
    public bool isMenuFlag = false;

    // �T�C�R���̊G
    public Image dice;
    bool isSelect = true;

    // �R���g���[���̃X�e�B�b�N�󂯓���
    float Vertical;

    // �ݒ�p�X���C�_�[
    public Slider sliderBgm;
    public Image handBgm;
    public Slider sliderSe;
    public Image handSe;

    // �^�C�g���̕���
    public Image startText;
    public Image configText;

    // �傫���Ȃ��Ă邩
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
        // �R���t�B�O���ł̓X�e�B�b�N���󂯕t���Ȃ�
        if (!isMenuFlag)
        {
            Vertical = Input.GetAxisRaw("L_Stick_V");
        }

        // �I���ړ�
        if (Vertical == -1 && isSelect)
        {
            // �I�����Ă���t�H���g���ł����Ȃ��Ă��邩
            isBig = true;
            // �R���t�B�O�I��
            isSelect = false;
            // �I���A�C�R���̃T�C�R���̈ʒu
            dice.transform.localPosition = new Vector3(-363.0f, -347.0f, -8.0f);
            // �I���A�C�R���̃T�C�R���̉�]
            dice.transform.Rotate(0, 0, 90);
        }
        else if (Vertical == 1 && !isSelect)
        {
            // �I�����Ă���t�H���g���ł����Ȃ��Ă��邩
            isBig = true;
            // �X�^�[�g�I��
            isSelect = true;
            // �I���A�C�R���̃T�C�R���̈ʒu
            dice.transform.localPosition = new Vector3(-514.0f, -178.0f, -8.0f);
            // �I���A�C�R���̃T�C�R���̉�]
            dice.transform.Rotate(0, 0, 90);
        }

        // �I�����Ă���t�H���g�g��
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

        // �V�[��Play�ֈړ�
        if ((Input.GetKeyDown("joystick button 0")) && !isMenuFlag && isSelect)
        {
            gMana.ChangeScene2("Play");
        }

        // �ݒ��ʂ̕\��
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

        // ���j���[��ʊ֌W
        CloseConfig();
        if (isMenuFlag)
        {
            OpenConfig();

            // BGM�グ����
            if (Input.GetKey("joystick button 5"))
            {
                UpBgm();
            }

            if (Input.GetKey("joystick button 4"))
            {
                DownBgm();
            }

            // LT,RT�̓���(InputManager�ɂ���)
            float tri = Input.GetAxis("L_R_Trigger");
            // SE�グ����
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

    // �ݒ��ʂ��J��
    void OpenConfig()
    {
        menuBox.SetActive(true);
    }

    // �ݒ��ʂ����
    void CloseConfig()
    {
        menuBox.SetActive(false);
    }

    // ���ʏグ��
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

    // ���ʉ�����
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

    // SE���グ��
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

    // SE��������
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
