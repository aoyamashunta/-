using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Rotation : MonoBehaviour
{
    [Header("‰ñ“]‘¬“x")]
    [Range(-1f, 1f)] public float Speed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, Speed));
    }
}
