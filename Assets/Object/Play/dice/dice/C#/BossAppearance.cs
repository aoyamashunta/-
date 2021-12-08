using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using UnityEngine.Playables;  
using UnityEngine.UI;
using UnityEditor;

public class BossAppearance : MonoBehaviour
{
    [Header("出現フラグ")]
    public bool IsAppearance = false;

    //ボス関係
    [Header("生成オブジェクト")]
    public GameObject Boss = default;
    public GameObject Dice = default;

    GameObject Instant_Boss = default;
    GameObject Instant_Dice = default;

    //出現エフェクト
    [Header("出現エフェクト")]
    public GameObject Effect = default;

    GameObject InstantEffect = default;

    //出現偏差
    [Header("偏差出現")]
    public float AppearanceFlame = 5f;
    float flame = 0f;


    [Header("出現前_Distance")]
    GameObject Player = default;
    public GameObject Dice_App = default;
    Vector3 posA = default;
    Vector3 posB = default;
    float dis = 0f;
    public float Appearance_Distance = 32f;

    [Header("イベントカメラ")]
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
        //出現前
        if(!IsAppearance)
        {
            //出現可能の距離
            Distance();
        }
        if (dis <= Appearance_Distance && !IsAppearance)
        {
            _director.Play();
        }

        //出現
        Appeearance();
    }

    //ボス出現
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

    //ボスまでの距離
    void Distance()
    {
        posA = Player.transform.position;
        posB = Dice_App.transform.position;
        dis = Vector3.Distance(posA, posB);

        //Debug.Log("距離："+dis);
    }

    //キャラストップ
    public void PlayerMoveStop()
    {
        playerControll.IsStick = true;
        playerControll.Horizontal = 0f;
        playerControll.Vertical = 0f;
    }

    //キャラスタート
    public void PlayerMoveStart()
    {
        playerControll.IsStick = false;

        Destroy(vcamera);
    }

}
