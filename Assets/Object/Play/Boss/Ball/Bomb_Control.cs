using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Bomb_Control : MonoBehaviour
{
    public GameObject Effect = default;
    GameObject Instant_Effect = default;

    [SerializeField]Vector3 _damageRangeCenter = default;
    [SerializeField]float _damageRangeRadius = 1f;


    private void Awake()
    {
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetDamageRangeCenter(), _damageRangeRadius);
    }

    //”ÍˆÍŒvŽZ
    Vector3 GetDamageRangeCenter()
    {
        Vector3 center = this.transform.position + this.transform.forward * _damageRangeCenter.z
            + this.transform.up * _damageRangeCenter.y
            + this.transform.right * _damageRangeCenter.x;
        return center;
    }



    void ExplosionDamage()
    {
        var cols = Physics.OverlapSphere(GetDamageRangeCenter(), _damageRangeRadius);
    
        
        foreach (var c in cols)
        {
            PlayerControll playerControll = c.gameObject.GetComponent<PlayerControll>();

            if (playerControll)
            {
                playerControll.Damage();
            }
        }
    }

    private void OnCollisionEnter(Collision collision){
 
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Ground_Rota"))
        {
            ExplosionDamage();
            Destroy(this.gameObject);
            Instant_Effect = Instantiate(Effect, this.transform.position, Quaternion.identity);
        }
    }
}
