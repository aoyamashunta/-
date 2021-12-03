using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niddle_Object_Control : MonoBehaviour
{
    GameObject Player = default;
    PlayerControll playerControll = default;

    RaycastHit hit;

    bool IsHit = false;
    [SerializeField]
	bool isEnable = true;

    [SerializeField]private GameObject Effect = default;

    private GameObject InstantObject = default;

    bool IsDamage = false;

    GameObject empthObject = default;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControll = Player.GetComponent<PlayerControll>();
    }

    void Update()
    {
        //Ray���΂��A���ɂ���I�u�W�F�N�g���擾����position�̎擾
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

            empthObject = new GameObject();
            empthObject.transform.parent = hit.collider.gameObject.transform;
            transform.parent = empthObject.transform;
        }
    }

    private void OnTriggerEnter(Collider collision){

        if (collision.CompareTag("Shield"))
        {
            if(playerControll.GetNormal_Shild_Attack1())
            {
                Destroy(this.gameObject);
                Destroy(empthObject);
                InstantObject = Instantiate(Effect, transform.position, Quaternion.identity);
            }
        }
    }

    void DamageOn()
    {
        IsDamage = true;
    }

    public bool GetDamageOn()
    {
        return IsDamage;
    }


    //IsNoDamage���I�t�̊�Shield�ڐG�R���C�_�[�Ƃ͕ʂ̃R���C�_�[��On�ɂ��Ƃ��ē����Ground���擾���č�������
    //IsNoDamage���I�t�̎���Gizmos���g������ɐڐG����Ground���擾����������(�ゾ�Ɩ��ʂȃR���|�[�l���g�����Inspector���킩��ɂ������邽�߂����̂Ō������ׂ�)
}
