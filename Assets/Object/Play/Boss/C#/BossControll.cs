using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControll : MonoBehaviour
{

    [Header("Dice�����t���O")]
    public bool IsDice = true;
    [Header("�U��")]
    public bool IsAttack = false;

    BossAttack1 bossAttack1 = default;
    BossAttack2 bossAttack2 = default;
    BossAttack3 bossAttack3 = default;

    Animator animator = default;

    //����
    bool IsFall_Down = false;
    //�_���[�W�\���
    bool IsDamageable_State = false;

    [Header("��������")]
    public float Down_Fall = 5f;

    GameObject Ball = default;
    Ball_Repal ball_Repal = default;

    GameObject Sword = default;
    Sword_Controll sword_Controll = default;

    void Start()
    {
        bossAttack1 = this.GetComponent<BossAttack1>();
        bossAttack2 = this.GetComponent<BossAttack2>();
        bossAttack3 = this.GetComponent<BossAttack3>();

        animator = this.GetComponent<Animator>();

        Ball = GameObject.FindGameObjectWithTag("Ball");
        ball_Repal = Ball.GetComponent<Ball_Repal>();

        Sword = GameObject.FindGameObjectWithTag("Sword");
        sword_Controll = Sword.GetComponent<Sword_Controll>();
    }


    void Update()
    {
        //�T�C�R�����@
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IsDice = false;
        }

        //�U���p�^�[��
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bossAttack1.IsStart = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bossAttack2.IsStart = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bossAttack3.IsStart = true;
        }


        Hit();

        Damage();

    }

    void LateUpdate()
    {
        animator.SetBool("IsDown", IsDamageable_State);
    }

    void Hit()
    {
        if (ball_Repal.GetIsHit())
        {
            IsFall_Down = true;
        }

        if (ball_Repal.GetIsHit())
        {
            Transform myTransform = this.transform;
 
            // ���W���擾
            Vector3 pos = myTransform.position;
 
            if(pos.y <= -Down_Fall)
            {
                pos.y = -Down_Fall;

                IsDamageable_State = true;
            }
            else if(pos.y >= -Down_Fall)
            {
                pos.y -= 0.1f;
            }

            myTransform.position = pos;  // ���W��ݒ�
        }
    }

    //�_���[�W�v�Z
    void Damage()
    {
        if (sword_Controll.GetIsBoss_Damage() && IsDamageable_State)
        {
            Debug.Log("�_���[�W");
            sword_Controll.IsBoss_Damage = false;
        }
    }
}
