using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceValue : MonoBehaviour
{
    [Header("ÉTÉCÉRÉçÇÃñ⁄")]
    [SerializeField]private int Number = 0;
    [SerializeField]private bool IsRoll = false;

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
        if(diceControll.flame == 1)
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
                X = 0;
                Y = 180;
                Z = 0;
            }
            else if(Number == 2)
            {
                X = 0;
                Y = 90;
                Z = 0;
            }
            else if(Number == 3)
            {
                X = 90;
                Y = 0;
                Z = 0;
            }
            else if(Number == 4)
            {
                X = -90;
                Y = 0;
                Z = 0;
            }
            else if(Number == 5)
            {
                X = 0;
                Y = -90;
                Z = 0;
            }
            else if(Number == 6)
            {
                X = 0;
                Y = 0;
                Z = 0;
            }

            this.gameObject.transform.rotation = Quaternion.Euler(X, Y, Z);

            Debug.Log("Number:"+Number);

            IsRoll = false;

        }
    }
}
