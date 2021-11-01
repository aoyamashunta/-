using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : MonoBehaviour
{

    //開始
    public bool IsStart = false;

    [Header("何ウェーブ打つか")]
    public int WaveMax = 3;
    private int WaveNumber = 0;

    //弾
    public GameObject bullet;

    //barrel
    public GameObject Barrel;

    //一度に打てる弾の総数
    [Header("発射総数")]
    public int BulletWayNum = 3;


    //弾の間隔
    [Header("発射弾との間隔")]
    public float BulletWaySpace = 30f;


    //角度
    [Header("発射弾の角度")]
    public float BulletWayAxis = 0f;


    //次弾の発射間隔
    [Header("次弾発射のタイム")]
    public float time = 1f;

    //最初の発射間隔
    [Header("最初弾発射のタイム")]
    public float delayTime = 1f;

    //現在のタイム
    float NowTime = 0f;


    void Start()
    {
        //タイムの初期化
        NowTime = delayTime;

        WaveNumber = 0;
    }


    void Update()
    {
        if(IsStart)
        {
            //タイム管理
            NowTime -= Time.deltaTime;

            //タイムが0になったら
            if(NowTime <= 0f)
            {
                //角度用変数
                float BulletWaySpeceSplit = 0f;

                //一回で発射する弾分ループ
                for(int i = 0; i < BulletWayNum; i++)
                {
                    //生成
                    CreateShotObject(BulletWaySpace - BulletWaySpeceSplit + BulletWayAxis - transform.localEulerAngles.y);

                    //角度調整
                    BulletWaySpeceSplit += (BulletWaySpace / (BulletWayNum - 1)) * 2;
                }
                //タイムの初期化
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
        //生成
        GameObject BulletClone = Instantiate(bullet, Barrel.transform.position, Quaternion.identity);

        //Bulletコンポーネント変数保存
        var BulletObject = BulletClone.GetComponent<EnemyBullet>();

        //弾を打ち出したオブジェクトの情報を渡す
        BulletObject.SetCharacterobject(gameObject);

        //角度変更
        BulletObject.SetForwardAxis(Quaternion.AngleAxis(axis, Vector3.up));
    }
}
