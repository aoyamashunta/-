using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Niddle : MonoBehaviour
{
    [SerializeField] GameObject Niddle = default;
    [SerializeField] GameObject CreatePos = default;

    [SerializeField] float CreateTime = 5f;
    float time = 0f;

    GameObject InstantObject = default;

    Quaternion CreateRotation = default;

    void Start()
    {
        CreateRotation = new Quaternion(-1f, 0f, 0f, 1f);
        InstantObject = Instantiate(Niddle, CreatePos.transform.position, CreateRotation);
        InstantObject.transform.parent = CreatePos.transform;
    }

    void Update()
    {
        if(InstantObject == null)
        {
            time = time + Time.deltaTime;
        }

        if(time >= CreateTime)
        {
            InstantObject = Instantiate(Niddle, CreatePos.transform.position, CreateRotation);
            InstantObject.transform.parent = CreatePos.transform;
            time = 0f;
        }
    }
}
