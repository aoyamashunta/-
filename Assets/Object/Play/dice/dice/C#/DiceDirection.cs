using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceDirection : MonoBehaviour
{
    GameObject Player = default;

    Vector3 vector3 = default;
    Quaternion quaternion = default;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        vector3 = Player.transform.position - this.transform.position;
        vector3.y = 0f;

        quaternion = Quaternion.LookRotation(-vector3);
        this.transform.rotation = quaternion;
    }
}
