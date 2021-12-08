using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using UnityEngine.Playables;  
using UnityEngine.UI;
using UnityEditor;

public class BossAppearance : MonoBehaviour
{
    [Header("�o���t���O")]
    public bool IsAppearance = false;

    //�{�X�֌W
    [Header("�����I�u�W�F�N�g")]
    public GameObject Boss = default;
    public GameObject Dice = default;

    GameObject Instant_Boss = default;
    GameObject Instant_Dice = default;

    //�o���G�t�F�N�g
    [Header("�o���G�t�F�N�g")]
    public GameObject Effect = default;

    GameObject InstantEffect = default;

    //�o���΍�
    [Header("�΍��o��")]
    public float AppearanceFlame = 5f;
    float flame = 0f;


    [Header("�o���O_Distance")]
    GameObject Player = default;
    public GameObject Dice_App = default;
    Vector3 posA = default;
    Vector3 posB = default;
    float dis = 0f;
    public float Appearance_Distance = 32f;

    [Header("�C�x���g�J����")]
    public GameObject vcamera = null;
    public PlayableDirector _director = default;


    BossControll bossControll = default;
    PlayerControll playerControll = default;

    [Header("Timeline")]
    [SerializeField]private GameObject BossDeadCut = null;

    public bool IsOn = false;



    void Start()
    {
        bossControll = Boss.GetComponent<BossControll>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();

        //Invoke("InvokeProcess", 5.0f);
    }

    void Update()
    {
        //�o���O
        if(!IsAppearance)
        {
            //�o���\�̋���
            Distance();
        }
        if (dis <= Appearance_Distance && !IsAppearance)
        {
            _director.Play();
        }

        //�o��
        Appeearance();
    }

    //�{�X�o��
    void Appeearance()
    {
        if (IsAppearance)
        {
            if(Instant_Boss == null && Instant_Dice == null){
                Destroy(Dice_App);
                flame++;

                if(flame == 1)InstantEffect = Instantiate(Effect, new Vector3(0, 2f, -5f), Quaternion.identity);
               
                if(flame >= AppearanceFlame){

                    Instant_Boss = Instantiate(Boss, new Vector3(0f, -2f, 0f), Quaternion.identity);
                    Instant_Dice = Instantiate(Dice, new Vector3(0f, 9.8f, 0f), Quaternion.identity);

                    Instantiate(BossDeadCut, new Vector3(-0.2f, -0.1f, -22.88f), Quaternion.identity);

                    bossControll.IsAttack = true;
                    IsOn = true;
                    flame = 0f;
                }
            }

        }
    }

    //�{�X�܂ł̋���
    void Distance()
    {
        posA = Player.transform.position;
        posB = Dice_App.transform.position;
        dis = Vector3.Distance(posA, posB);

        //Debug.Log("�����F"+dis);
    }

    //�L�����X�g�b�v
    public void PlayerMoveStop()
    {
        playerControll.IsStick = true;
        playerControll.Horizontal = 0f;
        playerControll.Vertical = 0f;
    }

    //�L�����X�^�[�g
    public void PlayerMoveStart()
    {
        playerControll.IsStick = false;

        Destroy(vcamera);
    }

}
