using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // G�}�l�[�W���[
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
        sliderSound.value = 0.5f;
        sliderSound.value = PlayerPrefs.GetFloat("BGMSlider");
    }

    // Update is called once per frame
    void Update()
    {
        //gMana.StartSound(gMana.BGM[0]);
        // �ݒ��ʂ̕\��
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown("joystick button 6")) && !isMenuFlag)
        {
            isMenuFlag = true;
        }
        else if((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown("joystick button 6")) && isMenuFlag)
        {
            isMenuFlag = false;
        }

        CloseConfig();
        if (isMenuFlag)
        {
            OpenConfig();

            UpSound();
            DownSound();
        }
    }

    // �ݒ��ʂ��J��
    void OpenConfig()
    {
        menu.enabled = true;
        menuBack.enabled = true;
        sliderLight.gameObject.SetActive(true);
        sliderSound.gameObject.SetActive(true);
      
    }

    // �ݒ��ʂ����
    void CloseConfig()
    {
        menu.enabled = false;
        menuBack.enabled = false;
        sliderLight.gameObject.SetActive(false);
        sliderSound.gameObject.SetActive(false);
    }

    // ���ʏグ��
    void UpSound()
    {
        if (Input.GetKey("joystick button 5"))
        {
            sliderSound.value += 0.01f;
            gMana.BGM[0].volume = sliderSound.value;
            PlayerPrefs.SetFloat("BGMVolume", gMana.BGM[0].volume);
            PlayerPrefs.SetFloat("BGMSlider", sliderSound.value);
        }
    }

    // ���ʉ�����
    void DownSound()
    {
        if (Input.GetKey("joystick button 4"))
        {
            sliderSound.value -= 0.01f;
            gMana.BGM[0].volume = sliderSound.value;
            PlayerPrefs.SetFloat("BGMVolume", gMana.BGM[0].volume);
            PlayerPrefs.SetFloat("BGMSlider", sliderSound.value);
        }
    }
}
