using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    [SerializeField]
    //ポーズした時に表示する
    private GameObject pauseUIPrefab;
    //ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    void Start()
    {
    }


    void Update()
    {
        if(Input.GetKeyDown("joystick button 7"))
        {
            if(pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
            }
        }
    }
}
