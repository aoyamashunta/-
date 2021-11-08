using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Controll : MonoBehaviour
{
    bool IsHit = false;
   
    void Start()
    {

    }

   
    void LateUpdate()
    {
        if (IsHit)
        {
            IsHit = false;
        }
    }

    private void OnTriggerEnter(Collider collision){
 
        if (collision.CompareTag("Ball"))
        {
            IsHit = true;
        }
    }

    public bool GetHit()
    {
        return IsHit;
    }
}
