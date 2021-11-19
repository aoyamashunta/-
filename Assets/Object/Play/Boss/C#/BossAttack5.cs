using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack5 : MonoBehaviour
{
    public bool IsStart = false;

    [Header("オブジェクト")]
    [SerializeField] private GameObject Niddle = default;

    [Header("Playerの範囲内での生成")]
    [SerializeField] private float R = 15f;

    [Header("生成間隔(S)")]
    [SerializeField] private int CreateTime = 1;

    [Header("生成総数")]
    [SerializeField] private int MaxNum = 1;


    GameObject InstantObject = default;
    GameObject Parent = default;


    private float time = 0;
    private float Num = 0;

    GameObject[] tagObject = default;


    //生成範囲
    private float x = 0;
    private float y = 0;
    private float z = 0;

    //プレイヤーを中心
    GameObject Player = default;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Parent = GameObject.Find("Ground4");
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
        tagObject = GameObject.FindGameObjectsWithTag("Niddle");
        //Debug.Log("Niddle数:"+tagObject.Length);

        time = time + Time.deltaTime;

        if (Num >= MaxNum)
        {
            IsStart = false;
            Num = 0;
            time = 0;
        }

        if (time > CreateTime)
        {
            Distance();
            InstantObject = Instantiate(Niddle, new Vector3(x, y - 20f, z), Niddle.transform.rotation);
            Num++;

            time = 0;
        }
    }

    void Distance()
    {
        x = Random.Range(Player.transform.position.x - R, Player.transform.position.x + R);
        y = Parent.transform.position.y;
        z = Random.Range(Player.transform.position.z - R, Player.transform.position.z + R);
    }

    public void Delete()
    {
        IsStart = false;
        time = 0;
        Num = 0;

        for(int i = 0; i < tagObject.Length;i++){
            Destroy(tagObject[i]);
        }
    }
}
