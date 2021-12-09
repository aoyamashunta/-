using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //弾速
    [Header("弾速")]
    [Range(1f, 10f)]public float Speed = 7f;


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

    //Player
    protected GameObject Player;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        //生成時に進行方向を決める
        if(Boss != null)
        {
            forward = Boss.transform.forward;
        }

        Player = GameObject.FindGameObjectWithTag("Player"); 
    }

    void Update()
    {
        //横移動
        forward.x += Time.deltaTime;

        //移動
        rb.velocity = forwardAxis * forward * Speed;

        //浮遊止め
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //消滅
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
