using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Controll : MonoBehaviour
{

    GameObject Player;
    PlayerControll playerControll;

    GameObject Boss;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();

        Boss = GameObject.FindGameObjectWithTag("Boss");
    }


    void Update()
    {

    }

    //アニメーションで敵の待機モーションで攻撃後抜けて、再び接触して攻撃が入ってしまう。

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "Boss" && playerControll.GetIsAttack_Motion())
        {
            Boss.GetComponent<BossControll>().Damage();

            //Debug.Log("接触");
        }
    }
}
