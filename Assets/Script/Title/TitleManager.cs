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
    }

    // Update is called once per frame
    void Update()
    {
        // �ݒ��ʂ̕\��
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

}
