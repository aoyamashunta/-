using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Repal : MonoBehaviour
{
    GameObject Shield = default;
    Shield_Controll shield_Controll = default;

    GameObject Boss = default;

    GameObject Player = default;
    PlayerControll playerControll = default;

    bool IsThrow = false;
    bool IsHit = false;

    Vector3 Ball_Pos = default;
    Vector3 Boss_Pos = default;
    Vector3 Player_Pos = default;

    bool IsRepel = false;

    [Header("ベジエ曲線")]
    public float L_R_Height = 20f;
    public float Frame = 80f;

    public float Center_Ratio = 0.50f;

    void Start()
    {
        Shield = GameObject.FindGameObjectWithTag("Shield");
        shield_Controll = Shield.GetComponent<Shield_Controll>();

        Boss = GameObject.FindGameObjectWithTag("Boss_Body");

        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();
    }


    void Update()
    {
        if (IsRepel && !IsThrow)
        {
            IsThrow = true;

            Ball_Pos = this.transform.position;
            Boss_Pos = Boss.transform.position;
            Player_Pos = Player.transform.position;
        }

        if (IsThrow)
        {
            StartThrow(this.gameObject, L_R_Height, Ball_Pos, Boss_Pos, Frame);
        }
    }

    //ベジェ曲線を用いたカーブ
    

    //(Target, 左右の高さ、開始地点、終了地点、補完速度（フレーム数）)
    public void StartThrow(GameObject Target,float height,Vector3 start,Vector3 end,float duration)
    {
        //中心点
        Vector3 half = end - start * Center_Ratio + start;//(0.5は0~1の中心)
        half.x -= Vector3.up.y + height;

        StartCoroutine(LerpThrow(Target, start, half, end, duration));
    }

    //コルーチン:Targetがstartからhalf,endまでdurationフレーム数かけて移動する
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

    //p0,p1を一次補完した位置a  p1,p2を一次補完した位置b　この二つを二次補完する
    Vector3 CalcLerpPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
        var a = Vector3.Lerp(p0, p1, t);
        var b = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(a, b, t);
    }

    private void OnTriggerStay(Collider collision){
 
        //SphereがPlaneと衝突している場合
        if (collision.gameObject.name == "Boss_Body_Cen")
        {
            //Debug.Log("ボスにダメージ");
            IsHit = true;
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Shield"))
        {
            if(playerControll.GetNormal_Shild_Attack1()){

                IsRepel = true;
            }
        }
    }

    public bool GetIsHit()
    {
        return IsHit;
    }
}
