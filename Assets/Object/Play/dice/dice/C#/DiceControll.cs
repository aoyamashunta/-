using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceControll : MonoBehaviour
{
    float nowPosi;

    [Header("通常時モード")]
    public bool IsNormal = false;

    [Header("停止")]
    public bool IsStop = false;

    [Header("回転速度(固定速度、速度、-速度、y軸変化フラグ)")]
    [SerializeField] private float FixedSpeed = 0f;
    [SerializeField] private float Speed = 8f;
    [SerializeField] private float MinusSpeed = 0f;
    [SerializeField] private bool IsChange = false;
    private float SpeedMax;
    //private bool Trigger = false;

    [Header("ノーマル回転速度(固定速度、速度")]
    [SerializeField] private float NormalFixedSpeed = 8f;

    [Header("投法回転速度(固定速度、速度)")]
    [SerializeField] private float ThrowFixedSpeed = 22f;


    [Header("初期状態へ移行")]
    public float flame = 0f;
    [SerializeField] private float MaxFlame = 100f;

    [Header("停止状態への移行")]
    [SerializeField] private float StopFlame = 100f;


    [Header("Effect")]
    public GameObject Effect = default;
    public GameObject EffectPos = default;

    GameObject InstantEffect = default;

    GameObject Boss;
    BossControll bossControll;


    void Start()
    {
        SpeedMax = 8f;

        nowPosi = this.transform.position.y;

        Boss = GameObject.FindGameObjectWithTag("Boss");
        bossControll = Boss.GetComponent<BossControll>();

    }

    void Update()
    {
        IsNormal = bossControll.IsDice;

        Normal();
        Throw();

        Ini();

        Roll();
    }

    void Normal()
    {
        if(IsNormal)
        {

            FixedSpeed         = NormalFixedSpeed;

            //上下運動
            transform.position = new Vector3(transform.position.x, nowPosi + Mathf.PingPong(Time.time/3, 0.3f), transform.position.z);
        }
    }

    void Throw()
    {
        if(!IsNormal && !IsStop)
        {
            flame++;

            FixedSpeed          = ThrowFixedSpeed;

            //上下運動
            transform.position = new Vector3(transform.position.x, nowPosi + Mathf.PingPong(Time.time/3, 0.3f), transform.position.z);

            //停止
            if(flame >= StopFlame)
            {
                flame = 0f;
                CreateEffect();
                IsStop = true;
            }
        }
    }

    void Ini()
    {
        if (IsStop)
        {
            flame++;

            if(flame >= MaxFlame)
            {
                DeleteEffect();

                IsStop = false;
                bossControll.IsDice = true;

                flame = 0f;

                this.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            }
        }
    }

    void Roll()
    {
        if(!IsStop)
        {
            //ランダム回転

            //Normal 8,8.1
            //Thrpw  22.20.6

            transform.Rotate(FixedSpeed, Speed, FixedSpeed);

            if (!IsChange)
            {
                Speed -= MinusSpeed;

                if (Speed <= -SpeedMax)
                {
                    IsChange = true;
                }
            }
            else if (IsChange)
            {
                Speed += MinusSpeed;

                if (Speed >= SpeedMax)
                {
                    IsChange = false;
                }
            }
        }
    }

    void CreateEffect()
    {
        //Effect生成
        InstantEffect = Instantiate(Effect, EffectPos.transform.position, Quaternion.identity);
    }

    void DeleteEffect()
    {
        Destroy(InstantEffect);
    }
}
