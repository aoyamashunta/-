using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack4 : MonoBehaviour
{
    public bool IsStart = false;

    [Header("�I�u�W�F�N�g")]
    [SerializeField] private GameObject Rock = default;

    [Header("Player�͈͓̔��ł̐���")]
    [SerializeField] private float R = 15f;

    [Header("�����Ԋu(S)")]
    [SerializeField] private float CreateTime = 1;

    [Header("��������")]
    [SerializeField] private int MaxNum = 1;


    private float time = 0;
    private float Num = 0;


    [Header("�������̍���")]
    [SerializeField]private float Min_Y = 80f;
    [SerializeField]private float Max_Y = 140f;

    //�����͈�
    private float x = 0;
    private float y = 0;
    private float z = 0;

    //�v���C���[�𒆐S
    GameObject Player = default;

    GameObject[] tagObject = default;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (IsStart)
        {
            Create();
        }
    }

    void Create()
    {
        tagObject = GameObject.FindGameObjectsWithTag("Rock");
        //Debug.Log("Rock��:"+tagObject.Length);

        time = time + Time.deltaTime;

        if (Num >= MaxNum)
        {
            Num = 0;
            time = 0;
            IsStart = false;
        }

        if (time > CreateTime)
        {
            Distance();

            Instantiate(Rock, new Vector3(x, y, z), Quaternion.identity);
            Num++;

            time = 0;
        }
    }

    void Distance()
    {
        x = Random.Range(Player.transform.position.x - R, Player.transform.position.x + R);
        y = Random.Range(Min_Y, Max_Y);
        z = Random.Range(Player.transform.position.z - R, Player.transform.position.z + R);
    }

    public void Delete()
    {
        IsStart = false;
        time = 0;
        Num = 0;

        if(tagObject != null){
            for(int i = 0; i < tagObject.Length;i++){
                Destroy(tagObject[i]);
            }
        }
    }
}
