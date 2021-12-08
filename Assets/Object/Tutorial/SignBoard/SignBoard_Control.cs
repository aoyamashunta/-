using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using UnityEngine.UI;

public class SignBoard_Control : MonoBehaviour
{
    public CinemachineVirtualCamera vCamera = default;
    //public GameObject Image1 = default;


    private void Start()
    {
        //Image1.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            vCamera.Priority = 15;
            //Image1.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            vCamera.Priority = 5;
            //Image1.SetActive(false);
        }
    }

}
