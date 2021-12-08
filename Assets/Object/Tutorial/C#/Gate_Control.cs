using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class Gate_Control : MonoBehaviour
{
    public GameObject Effect = default;
    GameObject Instant_Effect = default;

    public GameObject Gate = default;
    GameObject Instant_Gate = default;

    Vector3 pos = default;
    Quaternion rotation = default;

    public float flame = 0f;
    public bool IsStart = false;
    bool IsOpen = false;

    Animator _anim = default;


    void Start()
    {
        pos = this.transform.position;
        rotation = new Quaternion(0f, 1f, 0f, 1f);

        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            IsStart = true;
        }

        if (IsOpen)
        {
            flame++;

            if (flame == 1) Instant_Effect = Instantiate(Effect, pos, Quaternion.identity);
            else if (flame == 10)
            {
                Instant_Gate = Instantiate(Gate, pos, rotation);
                Destroy(this.gameObject);
                flame = 0;
                IsStart = false;
            }
        }
    }

    private void LateUpdate()
    {
        _anim.SetBool("IsOpen", IsStart);
    }

    public void Open()
    {
        IsOpen = true;
    }
}
