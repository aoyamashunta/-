using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRotation : MonoBehaviour
{
    float nowPosi;

    float angleX = 0f;
    float angleY = 0f;
    float angleZ = 0f;

    [Header("–ˆƒtƒŒ[ƒ€‚Ì‰ñ“]‘¬“x")]
    public float flame = 0.1f;

    void Start()
    {
        nowPosi = this.transform.position.y;
    }

    void Update()
    {
        //ã‰º‰^“®
        transform.position = new Vector3(transform.position.x, nowPosi + Mathf.PingPong(Time.time/3, 0.3f), transform.position.z);

        //ƒ‰ƒ“ƒ_ƒ€‰ñ“]
        Vector3 euler = transform.eulerAngles;

        angleX += flame*1f;
        angleY += flame*1f;
        angleZ += flame*1f;

        euler.z = angleZ;
        euler.y = angleY;
        euler.x = angleX;
        transform.eulerAngles = euler;
    }
}
