using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    //�e��
    [Header("�e��")]
    [Range(5f, 20f)]public float Reserve_Speed = 10f;
    [Range(1f, 30f)]public float TopSpeed = 15f;
    [Range(1f, 30f)]public float Homing_Speed = 15f;

    float speed = 0f;
    float ChangeFlame = 0f;

    [Header("�͈͋���")]
    public float dis = 5f;

    //���Ń^�C��
    [Header("���Ń^�C��")]
    [Range(1f, 50f)]public float time = 5f;


    //�i�s����
    protected Vector3 forward = new Vector3(1, 1, 1);

    //�ł��o���p�x
    protected Quaternion forwardAxis = Quaternion.identity;

    //Rigidbody
    protected Rigidbody rb;

    //�{�X�p�ϐ�
    protected GameObject Boss;

    protected GameObject Player;

    //�����O��i��~�܂Łj
    bool IsStart = false;

    //�z�[�~���O
    bool IsHoming = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();


        //�������ɐi�s���������߂�
        if(Boss != null)
        {
            forward = Boss.transform.forward;
        }

        speed = Reserve_Speed;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //�ړ��O
        if(!IsStart && !IsHoming)
        {
            Reserve();
        }

        Range_Player();

        //�z�[�~���O
        if(IsStart && !IsHoming)
        {
            No_Homing();
        }
        else if(IsStart && IsHoming)
        {
            Homing();
        }

        //����
        Delete();
    
    }

    //����
    void Reserve()
    {
        float minus = Random.Range(0.05f, 0.15f);

        if (Reserve_Speed >= 0f && ChangeFlame < 50)
        {

            speed = speed - minus;

            if (speed < 0f)
            {
                speed = 0f;

                ChangeFlame++;

                if (ChangeFlame >= 50)
                {
                    ChangeFlame = 50f;
                    speed = TopSpeed;
                    IsStart = true;
                }
            }
        }
        rb.velocity = forwardAxis * forward * speed;
    }

    //�z�[�~���O��
    void No_Homing()
    {
        rb.velocity = forwardAxis * forward * speed;
    }

    //�z�[�~���O����
    void Homing()
    {
        transform.position = Vector3.MoveTowards (this.transform.position,new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z), speed * Time.deltaTime);
    }

    //�͈͓��擾
    void Range_Player()
    {
        if(IsStart){
            Vector3 Target = Player.transform.position;
            float distance = Vector3.Distance(Target, this.transform.position);

            if(distance < dis)
            {
                IsHoming = true;
                speed = Homing_Speed;
            }
            else
            {
                IsHoming = false;
                speed = TopSpeed;
            }
        }
    }

    //�폜
    void Delete()
    {
        time -= Time.deltaTime;
        if(time <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.GetComponent<PlayerControll>().Damage();
            Destroy(this.gameObject);
        }
    }


    //�L�����N�^�[���̓n��
    public void SetCharacterobject(GameObject character)
    {
        this.Boss = character;
    }

    //�p�x��n��
    public void SetForwardAxis(Quaternion axis)
    {
        this.forwardAxis = axis;
    }

}
