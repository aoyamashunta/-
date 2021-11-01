using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Controll : MonoBehaviour
{
    GameObject Player;
    PlayerControll playerControll;

   
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();
    }

   
    void Update()
    {

    }

}
