using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//INHERITANCE
public class VerticalHazard : BasicHazard
{

    //POLYMORPHISM
    protected override Vector2 RecoilDirectionCalculator()
    {
        if (this.transform.position.x < PlayerRb2D.transform.position.x)
        {
            return Vector2.right;
        }
        return Vector2.left;
        
    }
}
