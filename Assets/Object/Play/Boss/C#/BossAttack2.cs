using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2 : MonoBehaviour
{
    //開始
    public bool IsStart = false;

    //弾
    public GameObject Bubble1;

    //barrel
    public GameObject Barrel;

    //一度に打てる弾の総数
    [Header("発射総数")]
    public int BulletWayNum = 1;
    public int BulletMaxNum = 5;
    int Num = 0;

    //弾の間隔
    [Header("発射弾の発射幅")]
    public float BulletWaySpace = 0f;
    [Range(-80f, 10f)]public float Min = -50f;
    [Range(10f, 80f)] public float Max =  50f;

    //角度
    [Header("発射弾の角度")]
    public float BulletWayAxis = 0f;


    //次弾の発射間隔
    [Header("次弾発射のタイム")]
    public float time = 10f;

    //最初の発射間隔
    [Header("最初弾発射のタイム")]
    public float delayTime = 1f;

    //現在のタイム
    float NowTime = 0f;


    void Start()
    {
        //タイムの初期化
        NowTime = delayTime;
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

                BulletWaySpace = Random.Range(Min, Max + 1f);

                //生成
                CreateShotObject(BulletWaySpace - BulletWaySpeceSplit + BulletWayAxis - transform.localEulerAngles.y);

                //角度調整
                BulletWaySpeceSplit += (BulletWaySpace / (BulletWayNum - 1)) * 2;

                Num += 1;

                if(Num >= BulletMaxNum)
                {
                    IsStart = false;
                    Num = 0;
                }

                //タイムの初期化
                NowTime = time;
            }
        }
    }

    private void CreateShotObject(float axis)
    {
        //生成
        GameObject BulletClone = Instantiate(Bubble1, Barrel.transform.position, Quaternion.identity);

        //Bulletコンポーネント変数保存
        var BulletObject = BulletClone.GetComponent<BubbleController>();

        //弾を打ち出したオブジェクトの情報を渡す
        BulletObject.SetCharacterobject(gameObject);

        //角度変更
        BulletObject.SetForwardAxis(Quaternion.AngleAxis(axis, Vector3.up));

    }
}
