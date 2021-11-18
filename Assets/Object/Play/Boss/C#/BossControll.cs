using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class BossControll : MonoBehaviour
{

    [Header("Dice投げフラグ")]
    public bool IsDice = true;
    [Header("攻撃")]
    public bool IsAttack = false;

    BossAttack1 bossAttack1 = default;
    BossAttack2 bossAttack2 = default;
    BossAttack3 bossAttack3 = default;
    BossAttack4 bossAttack4 = default;
    BossAttack5 bossAttack5 = default;

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
    public int HP;
    bool IsDamage = false;

    bool IsHit = false;
    bool IsWake_Up = false;

    [Header("バリア")]
    public GameObject Barrier = default;
    GameObject InstantObject = default;

    [Header("ダメージ時の無敵")]
    float flame = 0f;
    public float Invincible_MaxFlame = 5f;

    [Header("復帰時間")]
    public float Wake_Up_Time = 0f;
    public float Max_Wake_Time = 10f;

    [Header("ダイスロール")]
    [SerializeField]private float Roll_Interval = 5f;
    float time = 0f;

    //死亡
    public bool IsDead = false;
    float Camera_Change = 0f;

    [Header("専用カメラ")]
    public CinemachineVirtualCamera vCamera = default;

    void Start()
    {
        bossAttack1 = this.GetComponent<BossAttack1>();
        bossAttack2 = this.GetComponent<BossAttack2>();
        bossAttack3 = this.GetComponent<BossAttack3>();
        bossAttack4 = this.GetComponent<BossAttack4>();
        bossAttack5 = this.GetComponent<BossAttack5>();

        Dice = GameObject.FindGameObjectWithTag("Dice");
        diceValue = Dice.GetComponent<DiceValue>();

        animator = this.GetComponent<Animator>();

        HP = MaxHP;

        if(InstantObject == null)
        {
            InstantObject = Instantiate(Barrier, new Vector3(0,6,0),Quaternion.identity);
        }

        time = Roll_Interval / 2f;
    }


    void Update()
    {
        if(!IsDead){
            //ダイスロール
            //if (!IsHit && IsDice)
            //{
            //    time += Time.deltaTime;

            //    if (time >= Roll_Interval)
            //    {
            //        IsDice = false;
            //        time = 0f;
            //    }
            //}

            //攻撃パターン
            if (diceValue.GetNumber() == 1 || diceValue.GetNumber() == 6)
            {
                bossAttack1.IsStart = true;
                diceValue.Ini_Number();
            }
            else if(diceValue.GetNumber() == 2)
            {
                bossAttack2.IsStart = true;
                diceValue.Ini_Number();
            }
            else if(diceValue.GetNumber() == 3)
            {
                bossAttack3.IsStart = true;
                diceValue.Ini_Number();
            }
            else if (diceValue.GetNumber() == 4)
            {
                bossAttack4.IsStart = true;
                diceValue.Ini_Number();
            }
            else if(diceValue.GetNumber() == 5)
            {
                bossAttack5.IsStart = true;
                diceValue.Ini_Number();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                bossAttack3.IsStart = true;
            }

            //Barrier消失時の落下
            Fall_Down();
            //落下時間
            Down_Time();
            //起き上がり
            Wake_Up();
            //連続ダメージがないように無敵時間
            Invincible();
        

            if(HP <= 0)
            {
                IsDead = true;
            }
        }
        Dead();
    }

    void LateUpdate()
    {
        animator.SetBool("IsDamage", IsDamage);

        animator.SetBool("IsDead", IsDead);
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
            vCamera.Priority = 15;

            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            if (pos.y <= -Down_Fall)
            {
                pos.y = -Down_Fall;

                IsDamageable_State = true;
                vCamera.Priority = 5;
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
        //Debug.Log("BossHP:"+HP);
    }

    void Dead()
    {
        if (IsDead)
        {
            Destroy(InstantObject);
            vCamera.Priority = 15;

            if(Camera_Change < 4f){
                Camera_Change += Time.deltaTime;
            }
            else if(Camera_Change >= 2.5f)
            {
                vCamera.Priority = 5;
            }
        }
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

            //InstantObjectを消さないと次回の生成ができない
            if(InstantObject != null && !IsDead)
            {
                Destroy(InstantObject);
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

            //InstantObjectがnullなのを確認して生成
            Create_Barrier();
        }
    }

    void Create_Barrier()
    {
        if(InstantObject == null){
            Vector3 pos = new Vector3(0, 6, 0);
            InstantObject = Instantiate(Barrier, pos, Quaternion.identity);
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

    public bool GetIsHit()
    {
        return IsHit;
    }
}
