using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    //�e��
    [Header("�e��")]
    [Range(5f, 20f)]public float Speed = 10f;
    [Range(1f, 30f)]public float TopSpeed = 15f;

    float speed = 0f;
    float ChangeFlame = 0f;

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


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        //�������ɐi�s���������߂�
        if(Boss != null)
        {
            forward = Boss.transform.forward;
        }

        speed = Speed;
    }

    void Update()
    {
        float minus = Random.Range(0.1f, 0.25f);

        //�ړ�
        if(Speed >= 0f && ChangeFlame < 50)
        {
            
            speed = speed - minus;

            if(speed < 0f)
            {
                speed = 0f;

                ChangeFlame++;

                if(ChangeFlame >= 50)
                {
                    ChangeFlame = 50f;
                    speed = TopSpeed;
                }
            }
        }


        rb.velocity = forwardAxis * forward * speed;

        //����
        time -= Time.deltaTime;
        if(time <= 0f)
        {
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
