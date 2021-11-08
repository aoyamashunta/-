using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2 : MonoBehaviour
{
    //�J�n
    public bool IsStart = false;

    //�e
    public GameObject Bubble1;

    //barrel
    public GameObject Barrel;

    //��x�ɑłĂ�e�̑���
    [Header("���ˑ���")]
    public int BulletWayNum = 1;
    public int BulletMaxNum = 5;
    int Num = 0;

    //�e�̊Ԋu
    [Header("���˒e�̔��˕�")]
    public float BulletWaySpace = 0f;
    [Range(-80f, 10f)]public float Min = -50f;
    [Range(10f, 80f)] public float Max =  50f;

    //�p�x
    [Header("���˒e�̊p�x")]
    public float BulletWayAxis = 0f;


    //���e�̔��ˊԊu
    [Header("���e���˂̃^�C��")]
    public float time = 10f;

    //�ŏ��̔��ˊԊu
    [Header("�ŏ��e���˂̃^�C��")]
    public float delayTime = 1f;

    //���݂̃^�C��
    float NowTime = 0f;


    void Start()
    {
        //�^�C���̏�����
        NowTime = delayTime;
    }


    void Update()
    {
        if(IsStart)
        {
            //�^�C���Ǘ�
            NowTime -= Time.deltaTime;

            //�^�C����0�ɂȂ�����
            if(NowTime <= 0f)
            {
                //�p�x�p�ϐ�
                float BulletWaySpeceSplit = 0f;

                BulletWaySpace = Random.Range(Min, Max + 1f);

                //����
                CreateShotObject(BulletWaySpace - BulletWaySpeceSplit + BulletWayAxis - transform.localEulerAngles.y);

                //�p�x����
                BulletWaySpeceSplit += (BulletWaySpace / (BulletWayNum - 1)) * 2;

                Num += 1;

                if(Num >= BulletMaxNum)
                {
                    IsStart = false;
                    Num = 0;
                }

                //�^�C���̏�����
                NowTime = time;
            }
        }
    }

    private void CreateShotObject(float axis)
    {
        //����
        GameObject BulletClone = Instantiate(Bubble1, Barrel.transform.position, Quaternion.identity);

        //Bullet�R���|�[�l���g�ϐ��ۑ�
        var BulletObject = BulletClone.GetComponent<BubbleController>();

        //�e��ł��o�����I�u�W�F�N�g�̏���n��
        BulletObject.SetCharacterobject(gameObject);

        //�p�x�ύX
        BulletObject.SetForwardAxis(Quaternion.AngleAxis(axis, Vector3.up));

    }
}
