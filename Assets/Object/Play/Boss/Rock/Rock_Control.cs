using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Control : MonoBehaviour
{

    //ê⁄êGéûÇÃÉtÉâÉO
    private bool IsContact = false;


    GameObject Player = default;
    PlayerControll playerControll = default;

    [Header("Effect")]
    [SerializeField]private GameObject Rock_Effect = default;

    private GameObject InstantObject = default;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();
    }

    void Update()
    {
        Delete();
    }

    void Delete()
    {
        if (IsContact)
        {
            InstantObject = Instantiate(Rock_Effect, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Ground_Rota"))
        {
            //Debug.Log("ê⁄êG");
            IsContact = true;
        }

        if(!IsContact)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerControll.Damage();
                IsContact = true;
            }
        }
    }
}
