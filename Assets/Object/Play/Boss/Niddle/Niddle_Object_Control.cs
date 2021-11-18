using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niddle_Object_Control : MonoBehaviour
{
    GameObject Player = default;
    PlayerControll playerControll = default;

    bool IsNoDamage = false;

    RaycastHit hit;

    bool IsHit = false;
    [SerializeField]
	bool isEnable = true;

    [SerializeField]private GameObject Effect = default;

    private GameObject InstantObject = default;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();
    }

    void Update()
    {
        //Rayを飛ばし、上空にあるオブジェクトを取得且つpositionの取得
        if(isEnable){
            IsHit = Physics.Raycast(transform.position, Vector3.up, out hit, 100);

            if (IsHit)
            {
                RayContact("Ground");
                RayContact("Ground_Rota");
            }
            else if (!IsHit)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void RayContact( string name)
    {
        if (hit.collider.CompareTag(name))
        {
            this.transform.position = new Vector3(transform.position.x, hit.transform.position.y - 2.5f, transform.position.z);
            isEnable = false;
            var empthObject = new GameObject();
            empthObject.transform.parent = hit.collider.gameObject.transform;
            transform.parent = empthObject.transform;
        }
    }

    private void OnTriggerEnter(Collider collision){

        if (collision.CompareTag("Shield") && IsNoDamage)
        {
            if(playerControll.GetNormal_Shild_Attack1())
            {
                Destroy(this.gameObject);
                InstantObject = Instantiate(Effect, transform.position, Quaternion.identity);
            }
        }
    }

    public void Damage_On()
    {
        IsNoDamage = true;
    }

    //IsNoDamageがオフの間Shield接触コライダーとは別のコライダーをOnにしといて頭上のGroundを取得して高さ判定
    //IsNoDamageがオフの時にGizmosを使い頭上に接触したGroundを取得し高さ調整(上だと無駄なコンポーネントを作りInspectorをわかりにくくするためしたので検討すべき)
}
