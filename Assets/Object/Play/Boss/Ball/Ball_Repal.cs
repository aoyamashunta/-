using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Repal : MonoBehaviour
{
    GameObject Shield = default;
    Shield_Controll shield_Controll = default;

    GameObject Boss = default;
    GameObject BOSS = default;

    GameObject Player = default;
    PlayerControll playerControll = default;

    bool IsThrow = false;
    public bool IsHit = false;

    Vector3 Ball_Pos = default;
    Vector3 Boss_Pos = default;
    Vector3 Player_Pos = default;

    bool IsRepel = false;

    [Header("�x�W�G�Ȑ�")]
    public float L_R_Height = 20f;
    public float Frame = 80f;

    public float Center_Ratio = 0.50f;

    float PlayerAngle = 0f;
    bool IsTurn = false;

    [Header("Effect")]
    public GameObject Effect_Player = default;
    GameObject InstantEffect_Player = default;
    Vector3 HitPost_Player = default;

    public GameObject Effect_Boss = default;
    GameObject InstantEffect_Boss = default;
    Vector3 HitPos_Boss = default;


    void Start()
    {
        Shield = GameObject.FindGameObjectWithTag("Shield");
        shield_Controll = Shield.GetComponent<Shield_Controll>();

        Boss = GameObject.FindGameObjectWithTag("Boss_Body");
        BOSS = GameObject.FindGameObjectWithTag("Boss");

        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();
    }


    void Update()
    {
        if (IsRepel && !IsThrow)
        {
            PlayerAngle = Player.transform.eulerAngles.y * (Mathf.PI / 180.0f);

            //Debug.Log("Angle:"+PlayerAngle);

            if(PlayerAngle >= 0 && PlayerAngle <= 3.14)
            {
                //Debug.Log("�E");
                IsTurn = false;
            }
            else if(PlayerAngle >= 3.14 && PlayerAngle <= 6.3)
            {
                //Debug.Log("��");
                IsTurn = true;
            }

            IsThrow = true;

            Ball_Pos = this.transform.position;
            Boss_Pos = Boss.transform.position;
            Player_Pos = Player.transform.position;
        }

        if (IsThrow)
        {
            StartThrow(this.gameObject, L_R_Height, Ball_Pos, Boss_Pos, Frame);
        }

        if (IsHit)
        {
            BOSS.GetComponent<BossControll>().Hit();
            Destroy(this.gameObject);
        }
    }

    //�x�W�F�Ȑ���p�����J�[�u
    

    //(Target, ���E�̍����A�J�n�n�_�A�I���n�_�A�⊮���x�i�t���[�����j)
    public void StartThrow(GameObject Target,float height,Vector3 start,Vector3 end,float duration)
    {
        //���S�_
        Vector3 half = end - start * Center_Ratio + start;//(0.5��0~1�̒��S

        if(IsTurn)          half.x -= Vector3.up.y + height;
        else if(!IsTurn)    half.x += Vector3.up.y + height;

        StartCoroutine(LerpThrow(Target, start, half, end, duration));
    }

    //�R���[�`��:Target��start����half,end�܂�duration�t���[���������Ĉړ�����
    IEnumerator LerpThrow(GameObject target, Vector3 start, Vector3 half, Vector3 end, float duration)
    {
        float startTime = Time.timeSinceLevelLoad;
        float rate = 0f;
        while(true) 
        {
            if(rate >= 1.0f)
                yield break;

            float diff = Time.timeSinceLevelLoad - startTime;
            rate = diff / (duration / 60f);
            target.transform.position = CalcLerpPoint(start, half, end, rate);

            yield return null;
        }
    }

    //p0,p1���ꎟ�⊮�����ʒua  p1,p2���ꎟ�⊮�����ʒub�@���̓��񎟕⊮����
    Vector3 CalcLerpPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
        var a = Vector3.Lerp(p0, p1, t);
        var b = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(a, b, t);
    }


    private void OnTriggerEnter(Collider collision){
 
        //Sphere��Plane�ƏՓ˂��Ă���ꍇ
        if (collision.gameObject.name == "Boss_Body_Cen")
        {
            //Effect
            HitPos_Boss = collision.ClosestPointOnBounds(this.transform.position);
            CreateEffect_Boss(HitPos_Boss);

            IsHit = true;
        }

        if (collision.CompareTag("Shield"))
        {
            if(playerControll.GetNormal_Shild_Attack1())
            {
                //Effect
                HitPost_Player = collision.ClosestPointOnBounds(this.transform.position);
                CreateEffect_Player(HitPost_Player);

                IsRepel = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //��]
        if(transform.parent == null && other.gameObject.CompareTag("Ground_Rota"))
        {
            var empthObject = new GameObject();
            empthObject.transform.parent = other.gameObject.transform;
            transform.parent = empthObject.transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //��]
        if(transform.parent != null && other.gameObject.CompareTag("Ground_Rota"))
        {
            transform.parent = null;
        }
    }

    void CreateEffect_Player(Vector3 Hit)
    {
        InstantEffect_Player = Instantiate(Effect_Player, Hit, Quaternion.identity);
    }

    void CreateEffect_Boss(Vector3 Hit)
    {
        InstantEffect_Boss = Instantiate(Effect_Boss, Hit, Quaternion.identity);
    }

    public bool GetIsRepal()
    {
        return IsRepel;
    }
}
