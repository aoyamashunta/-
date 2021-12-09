using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;  // DOTween を使うため

public class Player_Life : MonoBehaviour
{
    //シリンダー
    public Slider _slider;

    //プレイヤーコンポーネント
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
        // DOTween.To() を使って連続的に変化させる
        DOTween.To(() => _slider.value, // 連続的に変化させる対象の値
            x => _slider.value = x, // 変化させた値 x をどう処理するかを書く
            value, // x をどの値まで変化させるか指示する
            _changeValueInterval);   // 何秒かけて変化させるか指示する
            //.SetEase(Ease.InOutBounce);
    }
}
