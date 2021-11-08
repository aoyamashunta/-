using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Player_Life : MonoBehaviour
{
    public Slider _slider;

    int MaxLife;
    int CurrentLife;

    PlayerControll playerControll;

    void Start()
    {
        _slider.value = 1;

        playerControll = GetComponent<PlayerControll>();

        MaxLife = playerControll.MaxHP;
        CurrentLife = MaxLife;
    }


    void Update()
    {
        if (playerControll.GetIsDamage())
        {
            CurrentLife = playerControll.GetCurrentHP();

            //”½‰f
            _slider.value = (float)CurrentLife / (float)MaxLife;
        }
    }
}
