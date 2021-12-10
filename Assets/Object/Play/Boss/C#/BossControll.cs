using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // DOTween を使うため

using Cinemachine;

public class BossControll : MonoBehaviour
{

    [Header("Dice投げフラグ")]
    public bool IsDice = true;
    [Header("攻撃")]
    public bool IsAttack = true;

    BossAttack1 bossAttack1 = default;
    BossAttack2 bossAttack2 = default;
    BossAttack3 bossAttack3 = default;
    BossAttack4 bossAttack4 = default;
    BossAttack5 bossAttack5 = default;
    BossAttack6 bossAttack6 = default;

    GameObject Dice = default;
    DiceControll diceControll = default;
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
    //public bool IsChange_Scene = false;

    [Header("専用カメラ")]
    public CinemachineVirtualCamera vCamera = default;
    CinemachineVirtualCamera InstantCamera = default;
    float ChangeTime = 0f;
    [SerializeField]private float MaxChangeTime = 2f;

    GameObject Player = default;

    //方向
    Vector3 vector3 = default;
    Quaternion quaternion = default;

    AudioSource audio;

    GameObject Hpslider;

    void Start()
    {
        bossAttack1 = this.GetComponent<BossAttack1>();
        bossAttack2 = this.GetComponent<BossAttack2>();
        bossAttack3 = this.GetComponent<BossAttack3>();
        bossAttack4 = this.GetComponent<BossAttack4>();
        bossAttack5 = this.GetComponent<BossAttack5>();
        bossAttack6 = this.GetComponent<BossAttack6>();

        Dice = GameObject.FindGameObjectWithTag("Dice");
        diceControll = Dice.GetComponent<DiceControll>();
        diceValue = Dice.GetComponent<DiceValue>();

        animator = this.GetComponent<Animator>();

        HP = MaxHP;

        if(InstantObject == null)
        {
            InstantObject = Instantiate(Barrier, new Vector3(0,6,0),Quaternion.identity);
        }

        time = -5f;

        InstantCamera = Instantiate(vCamera, new Vector3(-0.2773962f, 11.5f, -16.49009f), Quaternion.identity);
        InstantCamera.LookAt = this.gameObject.transform;
        //vCamera = CinemachineVirtualCamera.;

        Player = GameObject.FindGameObjectWithTag("Player");

        audio = this.GetComponent<AudioSource>();

        Hpslider = GameObject.FindGameObjectWithTag("bossHP");

        Hpslider.GetComponent<Slider>().value = (float)HP / MaxHP;
    }


    void Update()
    {

       
        if(!IsDead){

            //プレイヤーの向き
            vector3 = Player.transform.position - this.transform.position;
            vector3.y = 0f;
            quaternion = Quaternion.LookRotation(vector3);
            this.transform.rotation = quaternion;

            //ダイスロール
            if (!IsHit && IsDice && IsAttack)
            {
                time += Time.deltaTime;

                if (time >= Roll_Interval)
                {
                    IsDice = false;
                    time = 0f;
                }
            }

            //攻撃パターン
            Attack_Pattern();

            //Barrier消失時の落下
            Fall_Down();
            CameraChange();
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

    //ダメージ計算
    public void Damage()
    {

        if(IsHit && IsDamageable_State && !IsDamage) IsDamage = true;
        if(IsDamage)
        {
            HP -= 1;
           

            //Hpslider.GetComponent<Slider>().value = -0.5f;

            DOTween.To(() => Hpslider.GetComponent<Slider>().value, // 連続的に変化させる対象の値
              x => Hpslider.GetComponent<Slider>().value = x, // 変化させた値 x をどう処理するかを書く
             (float)HP / MaxHP, // x をどの値まで変化させるか指示する
              0.5f);   // 何秒かけて変化させるか指示する
        }
        //Debug.Log("BossHP:"+HP);
    }

    //攻撃パターン
    void Attack_Pattern()
    {
        if (!bossAttack1.IsStart)
        {
            audio.Stop();
        }

        if (diceValue.GetNumber() == 1)
        {
            bossAttack1.IsStart = true;
            diceValue.Ini_Number();

            // 炎の音
            audio.Play();
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
        else if(diceValue.GetNumber() == 6)
        {
            bossAttack6.IsStart = true;
            diceValue.Ini_Number();
        }

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

    //起きる
    void Wake_Up()
    {
        if (IsWake_Up && IsHit && IsDamageable_State)
        {
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            if (pos.y >= -UP_Fall)
            {
                pos.y = -UP_Fall;

                IsWake_Up = false;
                IsHit = false;
                ChangeTime = 0f;
                IsDamageable_State = false;

                //InstantObjectがnullなのを確認して生成
                Create_Barrier();
            }
            else if (pos.y < -UP_Fall)
            {
                pos.y += 0.25f;
            }

            myTransform.position = pos;  // 座標を設定
        }
    }

     //ダウン時のみ（死亡は違う）
    void CameraChange()
    {
        if (IsHit)
        {
            if(ChangeTime < MaxChangeTime)
            {
                InstantCamera.Priority = 15;
                ChangeTime += Time.deltaTime;
            }
            else if(ChangeTime >= MaxChangeTime)
            {
                InstantCamera.Priority = 5;
                ChangeTime = MaxChangeTime;
            }
        }
    }

    //ダウン時のみ
    public void Dead_Process()
    {
        StopCut();

        //Barrier
        Destroy(InstantObject);
        Down();
        Dead_Object();
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
            Down();

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

    void Down()
    {
        bossAttack1.Delete();
        bossAttack2.Delete();

        diceControll.Delete();
        diceValue.Delete();
    }

    //その他
    void Dead_Object()
    {
        bossAttack4.Delete();
        bossAttack5.Delete();
    }

    void Create_Barrier()
    {
        if(InstantObject == null){
            Vector3 pos = new Vector3(0, 6, 0);
            InstantObject = Instantiate(Barrier, pos, Quaternion.identity);
        }
    }

    public bool GetIsHit()
    {
        return IsHit;
    }

    //討伐カットシーン
    //低速
    void StopCut()
    {
        Time.timeScale = 0.7f;
    }

    public bool GetIsDamage()
    {
        return IsDamage;
    }
}
