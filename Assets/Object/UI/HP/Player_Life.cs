using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;  // DOTween ���g������

public class Player_Life : MonoBehaviour
{
    //�V�����_�[
    public Slider _slider;

    //�v���C���[�R���|�[�l���g
    PlayerControll playerControll;

    [SerializeField] float _changeValueInterval = 0.5f;


    public bool IsDead = false;

    void Start()
    {
        //_slider.value = 1;
    }

    private void Update()
    {
        //if(_slider.value <= 0)
        //{
        //    IsDead = true;
        //}
    }

    public void Change(float value)
    {
        ChangeValue(_slider.value + value);
    }

    public void Fill()
    {
        ChangeValue(1f);
    }



    void ChangeValue(float value)
    {
        // DOTween.To() ���g���ĘA���I�ɕω�������
        DOTween.To(() => _slider.value, // �A���I�ɕω�������Ώۂ̒l
            x => _slider.value = x, // �ω��������l x ���ǂ��������邩������
            value, // x ���ǂ̒l�܂ŕω������邩�w������
            _changeValueInterval);   // ���b�����ĕω������邩�w������
            //.SetEase(Ease.InOutBounce);
    }
}
