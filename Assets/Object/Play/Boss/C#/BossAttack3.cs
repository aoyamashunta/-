using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack3 : MonoBehaviour
{
    public GameObject Ball = default;
    public GameObject Barrel = default;

    public float Speed = 5f;

    public bool IsStart = false;

    public int BallNum = 0;
    public int BallMaxNum = 5;

    void Start()
    {

    }

    void Update()
    {
        if (IsStart)
        {
            Shot();
        }
    }

    public void Shot(){

        if(BallNum < BallMaxNum){
            BallNum+= 1;

            GameObject ballInstans =  (GameObject)Instantiate(Ball, Barrel.transform.position, Quaternion.identity);
            Rigidbody ballRigidbody = ballInstans.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(transform.forward * Speed);
        }
        else if(BallNum >= BallMaxNum)
        {
            IsStart = false;
            BallNum = 0;
        }
    }
}
