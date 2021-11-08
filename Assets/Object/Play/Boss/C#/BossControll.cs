using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControll : MonoBehaviour
{

    [Header("Dice投げフラグ")]
    public bool IsDice = true;
    [Header("攻撃")]
    public bool IsAttack = false;

    BossAttack1 bossAttack1 = default;
    BossAttack2 bossAttack2 = default;
    BossAttack3 bossAttack3 = default;

    GameObject Dice = default;
    DiceValue diceValue = default;

    Animator animator = default;

    //ダメージ可能状態
    bool IsDamageable_State = false;

    [Header("上昇距離")]
    public float UP_Fall = 3f;

    [Header("落下距離")]
    public float Down_Fall = 5f;

    [Header("体力")]
    public int MaxHP = 20;
    int HP;
    bool IsDamage = false;

    bool IsHit = false;
    bool IsWake_Up = false;

    [Header("ダメージ時の無敵")]
    float flame = 0f;
    public float Invincible_MaxFlame = 5f;

    [Header("復帰時間")]
    public float Wake_Up_Time = 0f;
    public float Max_Wake_Time = 10f;

    //bool IsDead = false;

    void Start()
    {
        bossAttack1 = this.GetComponent<BossAttack1>();
        bossAttack2 = this.GetComponent<BossAttack2>();
        bossAttack3 = this.GetComponent<BossAttack3>();

        Dice = GameObject.FindGameObjectWithTag("Dice");
        diceValue = Dice.GetComponent<DiceValue>();

        animator = this.GetComponent<Animator>();

        HP = MaxHP;
    }


    void Update()
    {
        //サイコロ投法
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IsDice = false;
        }

        //攻撃パターン
        if (diceValue.GetNumber() == 1 || diceValue.GetNumber() == 4)
        {
            bossAttack1.IsStart = true;
        }
        else if(diceValue.GetNumber() == 2 || diceValue.GetNumber() == 5)
        {
            bossAttack2.IsStart = true;
        }
        else if(diceValue.GetNumber() == 3 || diceValue.GetNumber() == 6)
        {
            bossAttack3.IsStart = true;
        }

        diceValue.Ini_Number();

        Fall_Down();


        Down_Time();

        Wake_Up();

        Invincible();
    }

    void LateUpdate()
    {
        animator.SetBool("IsDamage", IsDamage);
    }

    //接触
    public void Hit()
    {
        IsHit = true;
    }

    public bool GetHit()
    {
        return IsHit;
    }

    //落下
    void Fall_Down()
    {
        if (IsHit)
        {
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            if (pos.y <= -Down_Fall)
            {
                pos.y = -Down_Fall;

                IsDamageable_State = true;
            }
            else if (pos.y > -Down_Fall)
            {
                pos.y -= 0.1f;
            }

            myTransform.position = pos;  // 座標を設定
        }
    }

        //ダメージ計算
    public void Damage()
    {
        if(IsHit && IsDamageable_State && !IsDamage) IsDamage = true;
        if(IsDamage)
        {
            HP = HP - 1;
        }
        Debug.Log("BossHP:"+HP);
    }

    void Invincible()
    {
        if (IsDamage)
        {
            flame++;

            if(flame >= Invincible_MaxFlame)
            {
                flame = 0f;
                IsDamage = false;
                Debug.Log("解除");
            }
        }
    }

    //ダウン時、復帰まで
    void Down_Time()
    {
        if(IsHit && IsDamageable_State && !IsWake_Up)
        {
            Wake_Up_Time += Time.deltaTime;

            if(Wake_Up_Time >= Max_Wake_Time)
            {
                IsWake_Up = true;
                Wake_Up_Time = 0;
            }
        }
    }

    //起きる
    void Wake_Up()
    {
        if (IsWake_Up && IsHit && IsDamageable_State)
        {
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            pos.y = -UP_Fall;

            myTransform.position = pos;  // 座標を設定

            IsWake_Up = false;
            IsHit = false;
            IsDamageable_State = false;
        }
    }

    public bool GetIsDamage()
    {
        return IsDamage;
    }
    public int GetCurrentHP()
    {
        return HP;
    }
}
