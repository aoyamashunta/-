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
    [Header("�ړ����x")]
    public float Dush = 1f;
    public float Min = 8f;
    public float Max = 12f;

    [Header("�W�����v���̏���")]
    public float JumpPower = 500f;
    public float Attack_PlayerUp = 5f;
    bool IsJumping_ComboStop = false;

    [Header("�̗�")]
    public int MaxHP = 20;
    int HP;
    bool IsDamage = false;

    [Header("���A")]
    public float WakeUp_MaxTime = 5f;
    float WakeUp_Time = 0f;

    [Header("���G")]
    public float Invincible_MaxFlame = 5f;
    float Invincible_Flame = 0f;
    bool IsInvincible = false;

    //�U���̃^�C�v
    int AttackType = 0;

    [Header("Effect")]
    [Space]
    public ParticleSystem SordParticle = default;

    [Space]
    public ParticleSystem ShieldParticle = default;

    [Space]
    public ParticleSystem DushEffect = default;


    Animator anim = default;

    //�A�j���[�V�����Đ��t���O
    bool Normal_Attack1 = false;
    bool Normal_Attack2 = false;
    bool Normal_Shild_Attack1 = false;

    bool Jumping_Attack1 = false;
    bool Jumping_Attack2 = false;
    bool Jumping_Attack3 = false;
    bool Jumping_Attack4 = false;

    bool Idel = false;
    bool Jump_Motion = false;

    //�e��t���O
    bool IsWalk = false;
    bool IsSprint = false;
    bool IsAttack = false;
    bool IsJump = false;
    bool IsAttack_Motion = false;
 
    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        HP = MaxHP;
    }
 
    void Update() {
        //�ړ�

        if(!IsAttack){
            Move();
            Sprint();
        }

        Attack();
        //shield_Attack();

        Jump();


        //�_���[�W��̏���
        Wake_Up();
        Invincible();

        //�`��
        Effect();
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
            //DushEffect�Đ�
            if (!DushEffect.isEmitting)
            {
                DushEffect.Play();
            }
        }
        else if(!IsSprint)
        {
            //DushEffect��~
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

            if(!IsJump)//�ʏ�
            {
                AttackType = 1;
            }
            else if(IsJump)//��
            {
                //����
                if(!IsJumping_ComboStop)
                {
                    AttackType = 3;
                }
                else
                {
                    AttackType = 0;
                }
            }

            //Debug.Log("��");
        }
        else if(Input.GetKeyDown("joystick button 1"))
        {
            IsAttack = true;
            AttackType = 2;

            //Debug.Log("��");
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
            //DushEffect��~
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

        //���G�t�F�N�g�i�J�n���A�I�����̋�؂肪�����A���P�j
        if(Normal_Attack1 || Normal_Attack2 || Jumping_Attack1 || Jumping_Attack2 || Jumping_Attack3 || Jumping_Attack4)
        {
            SordParticle.Play(true);
            //Debug.Log("���G�t�F�N�g����");
        }
        else
        {
            SordParticle.Stop (true, ParticleSystemStopBehavior.StopEmitting);
        }

        //���G�t�F�N�g�i�J�n���A�I�����̋�؂肪�����A���P�j
        if(Normal_Shild_Attack1)
        {
            ShieldParticle.Play(true);
            //Debug.Log("�c�G�t�F�N�g����");
        }
        else
        {
            ShieldParticle.Stop (true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public void Damage()
    {
        if(!IsInvincible)IsDamage = true;
        if(IsDamage)HP = HP - 1;
    }

    void Wake_Up()
    {
        if (IsDamage)
        {
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
                IsInvincible = false;
            }
        }
    }

    //�A�j���\�V����
    void LateUpdate()
    {
        //�A�j���[�V�����Đ����t���O�̑��
        Normal_Attack1          = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1");
        Normal_Attack2          = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2");
        Normal_Shild_Attack1    = anim.GetCurrentAnimatorStateInfo(0).IsName("Shield_Attack1");

        Jumping_Attack1 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack1");
        Jumping_Attack2 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack2");
        Jumping_Attack3 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack3");
        Jumping_Attack4 = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Attack4");

        Idel = anim.GetCurrentAnimatorStateInfo(0).IsName("idel");
        Jump_Motion = anim.GetCurrentAnimatorStateInfo(0).IsName("Jump");

        anim.SetBool("Walk", IsWalk);
        anim.SetBool("Sprint", IsSprint);

        anim.SetBool("Jump", IsJump);

        //�A�j���[�V�����̃g���K�[
        if(IsAttack)anim.SetTrigger("Attack");
        anim.SetInteger("AttackType", AttackType);


        //�ʏ�U������
        if (AttackType != 0 && Idel)
        {
            AttackType = 0;
            IsAttack = false;
        }
        //�ʏ�U�����̈ړ����x�̌���
        if (Normal_Attack1 || Normal_Attack2 || Normal_Shild_Attack1)
        {
            moveSpeed = Dush;

            IsWalk = false;
            IsSprint = false;
        }
        //�W�����v�U������
        if(AttackType != 0 && Jump_Motion)
        {
            AttackType = 0;
            IsAttack = false;
        }
        //�U�����̈ړ����x�̌���
        else if(Jumping_Attack1 || Jumping_Attack2 || Jumping_Attack3 || Jumping_Attack4)
        {
            moveSpeed = Dush;
        }


        //�W�����v�U�����̏㏸
        if (Jumping_Attack1 || Jumping_Attack2 || Jumping_Attack3 || Jumping_Attack4)
        {
            rb.velocity = new Vector3(0, Attack_PlayerUp, 0);
        }

        //�R���{�Z���n�ʂɐڐG���Ȃ���Ăюg�p�ł��Ȃ�
        if (Jumping_Attack4)
        {
            IsJumping_ComboStop = true;
        }
    }

    void FixedUpdate() {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
 
        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * Vertical + Camera.main.transform.right * Horizontal;
 
        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
 
        // �L�����N�^�[�̌�����i�s������
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

    public bool GetIsDamage()
    {
        return IsDamage;
    }
    public int GetCurrentHP()
    {
        return HP;
    }
}
