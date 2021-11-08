using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceValue : MonoBehaviour
{
    [Header("ÉTÉCÉRÉçÇÃñ⁄")]
    [SerializeField]private int Number = 0;
    [SerializeField]private bool IsRoll = false;

    public bool IsConfirm = false;

    int X;
    int Y;
    int Z;

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

        Debug.Log("Number:"+Number);
    }

    public void Ini_Number()
    {
        Number = 0;
    }

    public int GetNumber()
    {
        return Number;
    }
}
