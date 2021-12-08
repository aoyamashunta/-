using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateColision : MonoBehaviour
{
    public bool isGateOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isGateOpen = true;
            Debug.Log("Playerê⁄êG");
        }
    }
}
