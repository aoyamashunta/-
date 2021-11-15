using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    float Horizontal;
    float Vertical;
    Rigidbody rb = default;

    [Space]
 
    float moveSpeed = 8f;
    [Header("移動速度")]
    public float Dush = 1f;
    public float Min = 8f;
    public float Max = 12f;

    [Header("ジャンプ時の処理")]
    public float JumpPower = 500f;
    public float Attack_PlayerUp = 5f;
    bool IsJumping_ComboStop = false;

    [Header("体力")]
    public int MaxHP = 20;
    int HP;
    Player_Life player_Life = default;

    //ダメージ
    bool IsDamage = false;
    bool IsBlinking = false;

    Transform child = default;
    Material Player_Color = default;


    [Header("復帰")]
    public float WakeUp_MaxTime = 5f;
    float WakeUp_Time = 0f;

    [Header("無敵")]
    public float Invincible_MaxFlame = 5f;
    float Invincible_Flame = 0f;
    bool IsInvincible = false;

    //攻撃のタイプ
    int AttackType = 0;

    [Header("Effect")]
    [Space]
    public ParticleSystem SordParticle = default;

    [Space]
    public ParticleSystem ShieldParticle = default;

    [Space]
    public ParticleSystem DushEffect = default;


    Animator anim = default;

    //アニメーション再生フラグ
    bool Normal_Attack1 = false;
    bool Normal_Attack2 = false;
    bool Normal_Attack3 = false;
    bool Normal_Attack4 = false;
    bool Normal_Shild_Attack1 = false;

    bool Jumping_Attack1 = false;
    bool Jumping_Attack2 = false;

    bool Idel = false;
    bool Jump_Motion = false;

    //各種フラグ
    bool IsWalk = false;
    bool IsSprint = false;
    bool IsAttack = false;
    bool IsJump = false;
    bool IsAttack_Motion = false;
 


    private void Awake()
    {
        child = transform.Find("RPGHero");
        Player_Color = child.GetComponent<Renderer>().material;
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        player_Life = GetComponent<Player_Life>();

        HP = MaxHP;
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


        //ダメージ後の処理
        Wake_Up();
        Invincible();

        //描画
        Effect();
        Blinking();
    }

    void Move()
    {
        if(!GetIsAttack_Motion() && !IsDamage)    
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
            
                moveSpeed = Max;
            }
            else
            {
                IsSprint = false;

                moveSpeed = Min;
            }
        }
        else
        {
            IsSprint = false;
        }

        if (IsSprint)
        {
            //DushEffect再生
            if (!DushEffect.isEmitting)
            {
                DushEffect.Play();
            }
        }
        else if(!IsSprint)
        {
            //DushEffect停止
            if (DushEffect.isEmitting)
            {
                DushEffect.Stop();
            }
        }
    }
 
    void Attack()
    {
        Input_Attack();
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
            //DushEffect停止
            if (DushEffect.isEmitting)
            {
                DushEffect.Stop();
            }

            IsWalk = false;
            IsSprint = false;
        }
    }

    void Effect()
    {

        //剣エフェクト（開始時、終了時の区切りが悪い、改善）
        if(Normal_Attack1 || Normal_Attack2 || Normal_Attack3 || Normal_Attack4 || Jumping_Attack1 || Jumping_Attack2)
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

    public void Damage()
    {
        if(!IsInvincible && !IsDamage){
            IsDamage = true;
            player_Life.Change(-0.1f);
        }
    }

    void Wake_Up()
    {
        if (IsDamage)
        {
            IsBlinking = true;

            WakeUp_Time += Time.deltaTime;

            moveSpeed = 0f;

            if(WakeUp_Time >= WakeUp_MaxTime)
            {
                IsDamage = false;
                WakeUp_Time = 0f;

                IsInvincible = true;
            }
        }
    }

    void Invincible()
    {
        if (IsInvincible)
        {
            Invincible_Flame++;

            if(Invincible_Flame >= Invincible_MaxFlame)
            {
                Invincible_Flame = 0f;
                IsBlinking = false;
                IsInvincible = false;
            }
        }
    }

    void Blinking()
    {
        if(IsBlinking)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));

            Player_Color.color = new Color(level, 0.0f, 0.0f, 1.0f);
        }
        else if (!IsBlinking)
        {
            Player_Color.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    //アニメ―ション
    void LateUpdate()
    {
        //アニメーション再生中フラグの代入
        Normal_Attack1          = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1");
        Normal_Attack2          = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2");
        Normal_Attack3          = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3");
        Normal_Attack4          = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack4");
        Normal_Shild_Attack1    = anim.GetCurrentAnimatorStateInfo(0).IsName("Shield_Attack1");

        Jumping_Attack1 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack1");
        Jumping_Attack2 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack2");

        Idel = anim.GetCurrentAnimatorStateInfo(0).IsName("idel");
        Jump_Motion = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump");

        anim.SetBool("Walk", IsWalk);
        anim.SetBool("Sprint", IsSprint);

        anim.SetBool("Jump", IsJump);

        //アニメーションのトリガー
        if(IsAttack)anim.SetTrigger("Attack");
        anim.SetInteger("AttackType", AttackType);


        //通常攻撃処理
        if (AttackType != 0 && Idel)
        {
            AttackType = 0;
            IsAttack = false;
        }
        //通常攻撃時の移動速度の減少
        if (Normal_Attack1 || Normal_Attack2 || Normal_Attack3 || Normal_Attack4 || Normal_Shild_Attack1)
        {
            moveSpeed = Dush;

            IsWalk = false;
            IsSprint = false;
        }

        //ジャンプ攻撃処理
        //攻撃時の移動速度の減少
        if (Jumping_Attack1 || Jumping_Attack2)
        {
            moveSpeed = Dush;
        }


        //ジャンプ攻撃時の上昇
        if (Jumping_Attack1 || Jumping_Attack2)
        {
            rb.velocity = new Vector3(0, Attack_PlayerUp, 0);
        }

        //コンボ技が地面に接触しなきゃ再び使用できない
        if (Jumping_Attack2)
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
            if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Ground_Rota"))
            {
                IsJump = false;
                IsJumping_ComboStop = false;
            }
        }

        //回転
        if(transform.parent == null && other.gameObject.CompareTag("Ground_Rota"))
        {
            var empthObject = new GameObject();
            empthObject.transform.parent = other.gameObject.transform;
            transform.parent = empthObject.transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //回転
        if(transform.parent != null && other.gameObject.CompareTag("Ground_Rota"))
        {
            transform.parent = null;
        }
    }


    public bool GetNormal_Shild_Attack1()
    {
        return Normal_Shild_Attack1;
    }

    public bool GetIsAttack_Motion()
    {
        if(Normal_Attack1 || Normal_Attack2 || Normal_Attack3 || Normal_Attack4 ||
            Normal_Shild_Attack1 || 
            Jumping_Attack1 || Jumping_Attack2)
        {
            IsAttack_Motion = true;
        }
        else
        {
            IsAttack_Motion = false;
        }

        return IsAttack_Motion;
    }

    public bool GetIsDamage()
    {
        return IsDamage;
    }
    public int GetCurrentHP()
    {
        return HP;
    }
}
