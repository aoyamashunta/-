using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceValue : MonoBehaviour
{
    [Header("サイコロの目")]
    [SerializeField]private int Number = 0;
    [SerializeField]private bool IsRoll = false;

    public bool IsConfirm = false;

    int X;
    int Y;
    int Z;

    //int Old_Number = 3;
    int[] Old_Numbers = new int[6];
    int array = -1;

    DiceControll diceControll;

    GameObject Player = default;

    Vector3 vector3 = default;
    Quaternion quaternion = default;

    void Start()
    {
        diceControll = this.gameObject.GetComponent<DiceControll>();

        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if(diceControll.flame == 1 && diceControll.IsStop)
        {
            IsRoll = true;
        }

        /*Rotation
        1: x:  0, y:180, z:  0
        2: x:  0, y: 90, z:  0
        3: x: 90, y:  0, z:  0
        4: x:-90, y:  0, z:  0
        5: x:  0, y:-90, z:  0
        6: x:  0, y:  0, z:  0
         */

        if (IsRoll)
        {
            Number = Random.Range(1, 7);
            array += 1;
            //Debug.Log(array);

            //同数時ループ
            for (int i = 0; i < 6; i++)
            {
                if(Old_Numbers[i] == Number){
                    while(Old_Numbers[i] == Number)
                    {
                        Number = Random.Range(1, 7);
                    }
                }
            }

            if(Number == 1)
            {
                Stop_Rotation(0, 180, 0);
            }
            else if(Number == 2)
            {
                Stop_Rotation(0, 90, 0);
            }
            else if(Number == 3)
            {
                Stop_Rotation(90, 0, 0);
            }
            else if(Number == 4)
            {
                Stop_Rotation(-90, 0, 0);
            }
            else if(Number == 5)
            {
                Stop_Rotation(0, -90, 0);
            }
            else if(Number == 6)
            {
                Stop_Rotation(0, 0, 0);
            }

            vector3 = Player.transform.position - this.transform.position;
            vector3.y = 0f;
            quaternion = Quaternion.LookRotation(-vector3);


            this.gameObject.transform.rotation = quaternion * Quaternion.Euler(X, Y, Z);

            IsRoll = false;

        }
    }


    void Stop_Rotation(int _X, int _Y, int _Z)
    {
        X = _X;
        Y = _Y;
        Z = _Z;
    }

    public void Ini_Number()
    {
        //Debug.Log("Number:"+Number);
        Old_Numbers[array] = Number;

        if(array == 5)
        {
            for(int i = 0; i < 6; i++)
            {
                Old_Numbers[i] = 0;
            }
            array = -1;
        }

        Number = 0;
    }

    public int GetNumber()
    {
        return Number;
    }

    public void Delete()
    {
        Number = 0;
        IsRoll = false;
    }
}
