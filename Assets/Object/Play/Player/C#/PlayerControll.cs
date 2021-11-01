using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    float Horizontal;
    float Vertical;
    Rigidbody rb;

    [Space]
 
    float moveSpeed = 8f;

    [Header("ジャンプ時の処理")]
    public float JumpPower = 500f;
    public float Attack_PlayerUp = 5f;
    bool IsJumping_ComboStop = false;

    bool IsWalk = false;
    bool IsSprint = false;
    bool IsAttack = false;
    bool IsJump = false;

    //攻撃のタイプ
    int AttackType = 0;

    Animator anim;

    [Space]
    public ParticleSystem SordParticle;

    [Space]
    public ParticleSystem ShieldParticle;


    //アニメーション再生フラグ
    bool Normal_Attack1;
    bool Normal_Attack2;
    bool Normal_Shild_Attack1;

    bool Jumping_Attack1;
    bool Jumping_Attack2;
    bool Jumping_Attack3;
    bool Jumping_Attack4;

    bool IsAttack_Motion = false;
 
    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
 
    void Update() {
        //移動
        if(!IsAttack){
            Move();
            Sprint();
        }

        Attack();
        //shield_Attack();

        Jump();

        //描画
        Effect();
    }

    void Move()
    {
        if(!GetIsAttack_Motion())    
        {
            Horizontal = Input.GetAxisRaw("L_Stick_H");
            Vertical = Input.GetAxisRaw("L_Stick_V");
        }

        if(Horizontal != 0.0f || Vertical != 0.0f)
        {
            IsWalk = true;
        }
        else if(Horizontal == 0.0f && Vertical == 0.0f)
        {
            IsWalk = false;
        }
    }

    void Sprint()
    {
        if (IsWalk)
        {
            if (Input.GetKey("joystick button 4"))
            {
                IsSprint = true;
            }
            else
            {
                IsSprint = false;
            }
        }
        else
        {
            IsSprint = false;
        }

        if (IsSprint)
        {
            moveSpeed = 12f;
        }
        else
        {
            moveSpeed = 8f;
        }
    }
 
    void Attack()
    {
        Input_Attack();

        if (!IsAttack)
        {
            AttackType = 0;
        }

        //Debug.Log("IsAttack:"+IsAttack);
    }
    void Input_Attack()
    {
        if(Input.GetKeyDown("joystick button 0"))
        {
            IsAttack = true;

            if(!IsJump)//通常
            {
                AttackType = 1;
            }
            else if(IsJump)//空中
            {
                //制限
                if(!IsJumping_ComboStop)
                {
                    AttackType = 3;
                }
                else
                {
                    AttackType = 0;
                }
            }

            //Debug.Log("剣");
        }
        else if(Input.GetKeyDown("joystick button 1"))
        {
            IsAttack = true;
            AttackType = 2;

            //Debug.Log("盾");
        }
        else
        {
            IsAttack = false;
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown("joystick button 2") && !IsJump)
        {
            IsJump = true;

            rb.AddForce(transform.up * JumpPower, ForceMode.Impulse);
        }

        if (IsJump)
        {
            IsWalk = false;
            IsSprint = false;
        }
    }

    void Effect()
    {

        //剣エフェクト（開始時、終了時の区切りが悪い、改善）
        if(Normal_Attack1 || Normal_Attack2 || Jumping_Attack1 || Jumping_Attack2 || Jumping_Attack3 || Jumping_Attack4)
        {
            SordParticle.Play(true);
            //Debug.Log("剣エフェクト発生");
        }
        else
        {
            SordParticle.Stop (true, ParticleSystemStopBehavior.StopEmitting);
        }

        //剣エフェクト（開始時、終了時の区切りが悪い、改善）
        if(Normal_Shild_Attack1)
        {
            ShieldParticle.Play(true);
            //Debug.Log("縦エフェクト発生");
        }
        else
        {
            ShieldParticle.Stop (true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    //アニメ―ション
    void LateUpdate()
    {
        //アニメーション再生中フラグの代入
        Normal_Attack1          = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1");
        Normal_Attack2          = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2");
        Normal_Shild_Attack1    = anim.GetCurrentAnimatorStateInfo(0).IsName("Shield_Attack1");

        Jumping_Attack1 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack1");
        Jumping_Attack2 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack2");
        Jumping_Attack3 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack3");
        Jumping_Attack4 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack4");



        anim.SetBool("Walk", IsWalk);
        anim.SetBool("Sprint", IsSprint);

        anim.SetBool("Jump", IsJump);

        //アニメーションのトリガー
        if(IsAttack)anim.SetTrigger("Attack");
        anim.SetInteger("AttackType", AttackType);

        //通常攻撃処理
        //通常攻撃時の移動速度の減少
        if (Normal_Attack1 || Normal_Attack2 || Normal_Shild_Attack1)
        {
            moveSpeed = 1f;

            IsWalk = false;
            IsSprint = false;
        }
        //ジャンプ攻撃処理
        //攻撃時の移動速度の減少
        else if(Jumping_Attack1 || Jumping_Attack2 || Jumping_Attack3 || Jumping_Attack4)
        {
            moveSpeed = 1f;
        }
        else
        {
            moveSpeed = 8f;
        }

        //ジャンプ攻撃時の上昇
        if (Jumping_Attack1 || Jumping_Attack2 || Jumping_Attack3 || Jumping_Attack4)
        {
            rb.velocity = new Vector3(0, Attack_PlayerUp, 0);
        }

        //コンボ技が地面に接触しなきゃ再び使用できない
        if (Jumping_Attack4)
        {
            IsJumping_ComboStop = true;
        }
    }

    void FixedUpdate() {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
 
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * Vertical + Camera.main.transform.right * Horizontal;
 
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
 
        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (IsJump)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                IsJump = false;
                IsJumping_ComboStop = false;
            }
        }
    }

    public bool GetNormal_Shild_Attack1()
    {
        return Normal_Shild_Attack1;
    }

    public bool GetIsAttack_Motion()
    {
        if(Normal_Attack1 || Normal_Attack2 || Normal_Shild_Attack1 || Jumping_Attack1 ||
            Jumping_Attack2 || Jumping_Attack3 || Jumping_Attack4)
        {
            IsAttack_Motion = true;
        }
        else
        {
            IsAttack_Motion = false;
        }

        return IsAttack_Motion;
    }
}
