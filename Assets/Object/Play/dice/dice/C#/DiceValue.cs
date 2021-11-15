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

    int Old_Number = 0;

    DiceControll diceControll;

    void Start()
    {
        diceControll = this.gameObject.GetComponent<DiceControll>();
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

            //同数時ループ
            if(Old_Number == Number)
            {
                while(Old_Number == Number)
                {
                    Number = Random.Range(1,7);
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

            this.gameObject.transform.rotation = Quaternion.Euler(X, Y, Z);

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

        Old_Number = Number;
        Number = 0;
    }

    public int GetNumber()
    {
        return Number;
    }
}
