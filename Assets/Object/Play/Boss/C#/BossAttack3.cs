using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack3 : MonoBehaviour
{
    public GameObject Ball = default;
    public GameObject Barrel = default;

    public float _speedRange = 500f;
    [Range(350f, 450f)]public float _speedMin =  400f;
    [Range(550f, 600f)] public float _speedMax =  550f;

    public bool IsStart = false;

    public int BallNum = 0;
    public int BallMaxNum = 5;

    //—””ÍˆÍ
    Vector3 _vectorRange = default;
    [Range(-0.1f, -1f)]public float _vectorMin = -1f;
    [Range(0.1f, 1f)] public float _vectorMax =  1f;
    float deviation = 0.3f;

    void Start()
    {
        //0.3‚ª’†SˆÊ’uAy‚Í1ˆÈãã‚°‚È‚¢
        _vectorRange  = new Vector3(0 + deviation, 1, 0);
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

            _vectorRange = new Vector3(Random.Range(_vectorMin, _vectorMax+0.1f)+deviation, 1, 0);
            _speedRange = Random.Range(_speedMin, _speedMax+1);

            GameObject ballInstans =  (GameObject)Instantiate(Ball, Barrel.transform.position, Quaternion.identity);
            Rigidbody ballRigidbody = ballInstans.GetComponent<Rigidbody>();
            ballRigidbody.AddForce((transform.forward + _vectorRange) * _speedRange);
        }
        else if(BallNum >= BallMaxNum)
        {
            IsStart = false;
            BallNum = 0;
        }
    }
}
