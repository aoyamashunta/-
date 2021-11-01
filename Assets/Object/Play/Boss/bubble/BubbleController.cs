using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    //弾速
    [Header("弾速")]
    [Range(5f, 20f)]public float Speed = 10f;
    [Range(1f, 30f)]public float TopSpeed = 15f;

    float speed = 0f;
    float ChangeFlame = 0f;

    //消滅タイム
    [Header("消滅タイム")]
    [Range(1f, 50f)]public float time = 5f;


    //進行方向
    protected Vector3 forward = new Vector3(1, 1, 1);

    //打ち出す角度
    protected Quaternion forwardAxis = Quaternion.identity;

    //Rigidbody
    protected Rigidbody rb;

    //ボス用変数
    protected GameObject Boss;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        //生成時に進行方向を決める
        if(Boss != null)
        {
            forward = Boss.transform.forward;
        }

        speed = Speed;
    }

    void Update()
    {
        float minus = Random.Range(0.1f, 0.25f);

        //移動
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

        //消滅
        time -= Time.deltaTime;
        if(time <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    //キャラクター情報の渡す
    public void SetCharacterobject(GameObject character)
    {
        this.Boss = character;
    }

    //角度を渡す
    public void SetForwardAxis(Quaternion axis)
    {
        this.forwardAxis = axis;
    }
}
