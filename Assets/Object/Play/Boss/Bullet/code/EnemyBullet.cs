using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //�e��
    [Header("�e��")]
    [Range(1f, 10f)]public float Speed = 7f;


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

    //Player
    protected GameObject Player;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        //�������ɐi�s���������߂�
        if(Boss != null)
        {
            forward = Boss.transform.forward;
        }

        Player = GameObject.FindGameObjectWithTag("Player"); 
    }

    void Update()
    {
        //���ړ�
        forward.x += Time.deltaTime;

        //�ړ�
        rb.velocity = forwardAxis * forward * Speed;

        //���V�~��
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //����
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
