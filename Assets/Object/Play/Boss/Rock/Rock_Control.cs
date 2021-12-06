using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Control : MonoBehaviour
{

    //ê⁄êGéûÇÃÉtÉâÉO
    private bool IsContact = false;


    GameObject Player = default;
    PlayerControll playerControll = default;

    GameObject Boss = default;

    [Header("Effect")]
    [SerializeField]private GameObject Rock_Effect = default;

    private GameObject InstantObject = default;

    //âe
    [Header("âe")]
    RaycastHit hit = default;
    bool IsHit = false;
    [SerializeField] private GameObject Shadow = null;
    GameObject InstantShadow = null;

    float time = 0f;
    float Max_time = 8f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();

        Boss = GameObject.FindGameObjectWithTag("Boss");
    }

    void Update()
    {
        IsHit = Physics.Raycast(transform.position, Vector3.down, out hit, 200);

        if (IsHit)
        {
            RayContact("Ground");
            RayContact("Ground_Rota");
        }

        Delete();

        time += Time.deltaTime;
        if(time >= Max_time)
        {
            Destroy(this.gameObject);
        }
    }

    void RayContact( string name)
    {
        if(InstantShadow == null)
        {
            InstantShadow = Instantiate(Shadow, this.transform.position, Quaternion.identity);
        }

        if (hit.collider.CompareTag(name))
        {
            InstantShadow.transform.position = new Vector3(transform.position.x, hit.transform.position.y - 0.9f, transform.position.z);
        }
    }

    void Delete()
    {
        if (IsContact)
        {
            InstantObject = Instantiate(Rock_Effect, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
            Destroy(InstantShadow);
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
            else if (other.gameObject.CompareTag("Boss"))
            {
                IsContact = true;
                Boss.GetComponent<BossControll>().Hit();
            }
        }
    }
}
