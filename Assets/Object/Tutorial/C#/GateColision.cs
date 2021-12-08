using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateColision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("PlayerÚG");
        }
    }
}
