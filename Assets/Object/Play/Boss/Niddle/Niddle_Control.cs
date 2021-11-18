using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niddle_Control : MonoBehaviour
{
    [SerializeField]Vector3 _damageRangeCenter = default;
    [SerializeField]float _damageRangeRadius = 1f;

    [SerializeField]float Power = 15f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetDamageRangeCenter(), _damageRangeRadius);
    }

    //範囲計算
    Vector3 GetDamageRangeCenter()
    {
        Vector3 center = this.transform.position + this.transform.forward * _damageRangeCenter.z
            + this.transform.up * _damageRangeCenter.y
            + this.transform.right * _damageRangeCenter.x;
        return center;
    }

    void Update()
    {
        //範囲内にコライダー入っているか
        var cols = Physics.OverlapSphere(GetDamageRangeCenter(), _damageRangeRadius);
    
        
        foreach (var c in cols)
        {
            PlayerControll playerControll = c.gameObject.GetComponent<PlayerControll>();
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
            Vector3 up = c.transform.up;

            if (playerControll)
            {
                //Debug.Log("NiddleDamage");
                rb.velocity = up * Power;
                playerControll.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
