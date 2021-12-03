using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    GameObject targetObj;
    Vector3 targetPos;

    public float angleSpeed = 200f;
 
    void Start () {
        targetObj = GameObject.FindGameObjectWithTag("Player");
        targetPos = targetObj.transform.position;
    }
 
    void Update() {
        // target�̈ړ��ʕ��A�����i�J�����j���ړ�����
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
 
        // �}�E�X�̈ړ���
        float Horizontal = Input.GetAxis("R_Stick_H");
        //float Vertical = Input.GetAxis("R_Stick_V");
        //Debug.Log(Vertical);

        // target�̈ʒu��Y���𒆐S�ɁA��]�i���]�j����
        transform.RotateAround(targetPos, Vector3.up, Horizontal * Time.deltaTime * angleSpeed);
        // �J�����̐����ړ��i���p�x�����Ȃ��A�K�v��������΃R�����g�A�E�g�j
        //transform.RotateAround(targetPos, transform.right, -(Vertical) * Time.deltaTime * 10f);
    }

}
