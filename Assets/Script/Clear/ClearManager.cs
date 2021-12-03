using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearManager : MonoBehaviour
{
    // Gマネージャー
    public GameManager gMana;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
        {
            gMana.ChangeScene2("Title");
        }
    }
}
