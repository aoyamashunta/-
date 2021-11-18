using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    //弾速
    [Header("弾速")]
    [Range(5f, 20f)]public float Reserve_Speed = 10f;
    [Range(1f, 30f)]public float TopSpeed = 15f;
    [Range(1f, 30f)]public float Homing_Speed = 15f;

    float speed = 0f;
    float ChangeFlame = 0f;

    [Header("範囲距離")]
    public float dis = 5f;

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

    protected GameObject Player;

    //準備前後（停止まで）
    bool IsStart = false;

    //ホーミング
    bool IsHoming = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();


        //生成時に進行方向を決める
        if(Boss != null)
        {
            forward = Boss.transform.forward;
        }

        speed = Reserve_Speed;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //移動前
        if(!IsStart && !IsHoming)
        {
            Reserve();
        }

        Range_Player();

        //ホーミング
        if(IsStart && !IsHoming)
        {
            No_Homing();
        }
        else if(IsStart && IsHoming)
        {
            Homing();
        }

        //消滅
        Delete();
    
    }

    //準備
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

    //ホーミング無
    void No_Homing()
    {
        rb.velocity = forwardAxis * forward * speed;
    }

    //ホーミングあり
    void Homing()
    {
        transform.position = Vector3.MoveTowards (this.transform.position,new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z), speed * Time.deltaTime);
    }

    //範囲内取得
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

    //削除
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
