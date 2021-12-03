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
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
 
        // マウスの移動量
        float Horizontal = Input.GetAxis("R_Stick_H");
        //float Vertical = Input.GetAxis("R_Stick_V");
        //Debug.Log(Vertical);

        // targetの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(targetPos, Vector3.up, Horizontal * Time.deltaTime * angleSpeed);
        // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
        //transform.RotateAround(targetPos, transform.right, -(Vertical) * Time.deltaTime * 10f);
    }

}
