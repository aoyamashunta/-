using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Controll : MonoBehaviour
{

    GameObject Player;
    PlayerControll playerControll;

    //Bossにダメージを与える
    public bool IsBoss_Damage = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();
    }


    void Update()
    {
        
    }

    //アニメーションで敵の待機モーションで攻撃後抜けて、再び接触して攻撃が入ってしまう。

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "Boss" && playerControll.GetIsAttack_Motion())
        {
            IsBoss_Damage = true;

            Debug.Log("接触");
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

                Debug.Log("接触終了");
            }
        }
    }
}
