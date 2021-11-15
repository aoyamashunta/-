using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_Break : MonoBehaviour
{

    GameObject Boss = default;
    BossControll bossControll = default;

    [Header("Effect")]
    public GameObject Effect = default;

    GameObject InstantObject = default;

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        bossControll = Boss.GetComponent<BossControll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossControll.GetIsHit())
        {
            InstantObject = Instantiate(Effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
