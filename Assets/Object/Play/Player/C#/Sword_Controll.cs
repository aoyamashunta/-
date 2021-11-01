using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Controll : MonoBehaviour
{

    GameObject Player;
    PlayerControll playerControll;

    //Boss�Ƀ_���[�W��^����
    public bool IsBoss_Damage = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();
    }


    void Update()
    {
        
    }

    //�A�j���[�V�����œG�̑ҋ@���[�V�����ōU���㔲���āA�ĂѐڐG���čU���������Ă��܂��B

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "Boss" && playerControll.GetIsAttack_Motion())
        {
            IsBoss_Damage = true;

            Debug.Log("�ڐG");
        }
    }

    public bool GetIsBoss_Damage()
    {
        return IsBoss_Damage;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Boss" && playerControll.GetIsAttack_Motion())
        {
            if (IsBoss_Damage)
            {
                IsBoss_Damage = false;

                Debug.Log("�ڐG�I��");
            }
        }
    }
}
