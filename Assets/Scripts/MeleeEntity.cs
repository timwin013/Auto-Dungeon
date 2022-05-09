using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntity : BaseUnit
{
    public void Update()
    {
        //if (!HasEnemy)
        //{
        //    FindTarget();
        //}
        //if (!HasEnemy)
        //{
        //    return;
        //}

        //if(IsInRange && !moving)
        //{
            // attack
            //Debug.Log("attack!");
        //}
        //else
        //if (IsInRange){

        //}
        //else{
        if (HasEnemy){
            GetInRange();
        }
        //}
    }
}