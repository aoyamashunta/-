using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceControll : MonoBehaviour
{
    float nowPosi;

    [Header("�ʏ펞���[�h")]
    public bool IsNormal = false;

    [Header("��~")]
    public bool IsStop = false;

    [Header("��]���x(�Œ葬�x�A���x�A-���x�Ay���ω��t���O)")]
    [SerializeField] private float FixedSpeed = 0f;
    [SerializeField] private float Speed = 8f;
    [SerializeField] private float MinusSpeed = 0f;
    [SerializeField] private bool IsChange = false;
    private float SpeedMax;
    //private bool Trigger = false;

    [Header("�m�[�}����]���x(�Œ葬�x�A���x")]
    [SerializeField] private float NormalFixedSpeed = 8f;

    [Header("���@��]���x(�Œ葬�x�A���x)")]
    [SerializeField] private float ThrowFixedSpeed = 22f;


    [Header("������Ԃֈڍs")]
    public float flame = 0f;
    [SerializeField] private float MaxFlame = 100f;

    [Header("��~��Ԃւ̈ڍs")]
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

            //�㉺�^��
            transform.position = new Vector3(transform.position.x, nowPosi + Mathf.PingPong(Time.time/3, 0.3f), transform.position.z);
        }
    }

    void Throw()
    {
        if(!IsNormal && !IsStop)
        {
            flame++;

            FixedSpeed          = ThrowFixedSpeed;

            //�㉺�^��
            transform.position = new Vector3(transform.position.x, nowPosi + Mathf.PingPong(Time.time/3, 0.3f), transform.position.z);

            //��~
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
            //�����_����]

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
        //Effect����
        InstantEffect = Instantiate(Effect, EffectPos.transform.position, Quaternion.identity);
    }

    void DeleteEffect()
    {
        Destroy(InstantEffect);
    }
}
