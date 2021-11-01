using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : MonoBehaviour
{

    //�J�n
    public bool IsStart = false;

    [Header("���E�F�[�u�ł�")]
    public int WaveMax = 3;
    private int WaveNumber = 0;

    //�e
    public GameObject bullet;

    //barrel
    public GameObject Barrel;

    //��x�ɑłĂ�e�̑���
    [Header("���ˑ���")]
    public int BulletWayNum = 3;


    //�e�̊Ԋu
    [Header("���˒e�Ƃ̊Ԋu")]
    public float BulletWaySpace = 30f;


    //�p�x
    [Header("���˒e�̊p�x")]
    public float BulletWayAxis = 0f;


    //���e�̔��ˊԊu
    [Header("���e���˂̃^�C��")]
    public float time = 1f;

    //�ŏ��̔��ˊԊu
    [Header("�ŏ��e���˂̃^�C��")]
    public float delayTime = 1f;

    //���݂̃^�C��
    float NowTime = 0f;


    void Start()
    {
        //�^�C���̏�����
        NowTime = delayTime;

        WaveNumber = 0;
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

                //���Ŕ��˂���e�����[�v
                for(int i = 0; i < BulletWayNum; i++)
                {
                    //����
                    CreateShotObject(BulletWaySpace - BulletWaySpeceSplit + BulletWayAxis - transform.localEulerAngles.y);

                    //�p�x����
                    BulletWaySpeceSplit += (BulletWaySpace / (BulletWayNum - 1)) * 2;
                }
                //�^�C���̏�����
                NowTime = time;

                WaveNumber += 1;

                if(WaveNumber >= WaveMax)
                {
                    IsStart = false;
                    NowTime = time;
                    WaveNumber = 0;
                }
            }
        }
    }

    private void CreateShotObject(float axis)
    {
        //����
        GameObject BulletClone = Instantiate(bullet, Barrel.transform.position, Quaternion.identity);

        //Bullet�R���|�[�l���g�ϐ��ۑ�
        var BulletObject = BulletClone.GetComponent<EnemyBullet>();

        //�e��ł��o�����I�u�W�F�N�g�̏���n��
        BulletObject.SetCharacterobject(gameObject);

        //�p�x�ύX
        BulletObject.SetForwardAxis(Quaternion.AngleAxis(axis, Vector3.up));
    }
}
