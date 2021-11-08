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

    //�A�j���[�V�����œG�̑ҋ@���[�V�����ōU���㔲���āA�ĂѐڐG���čU���������Ă��܂��B

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "Boss" && playerControll.GetIsAttack_Motion())
        {
            Boss.GetComponent<BossControll>().Damage();

            //Debug.Log("�ڐG");
        }
    }
}
