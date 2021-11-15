using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_Control : MonoBehaviour
{
    [Header("‰ñ“]‘¬“x")]
    [Range(-5f, 5f)] public float Speed = 1f;

    GameObject Boss = default;
    BossControll bossControll = default;

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        bossControll = Boss.GetComponent<BossControll>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!bossControll.GetIsHit()){
            transform.Rotate(new Vector3(0f, Speed,0f));
        }
    }
}
