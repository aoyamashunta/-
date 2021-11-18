using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Control : MonoBehaviour
{

    //�ڐG���̃t���O
    private bool IsContact = false;


    GameObject Player = default;
    PlayerControll playerControll = default;

    [Header("Effect")]
    [SerializeField]private GameObject Rock_Effect = default;

    private GameObject InstantObject = default;

    float time = 0f;
    float Max_time = 8f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();
    }

    void Update()
    {
        Delete();

        time += Time.deltaTime;
        if(time >= Max_time)
        {
            Destroy(this.gameObject);
        }
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
            //Debug.Log("�ڐG");
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
