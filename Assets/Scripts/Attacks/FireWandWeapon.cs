using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWandWeapon : BowWeapon
{
    private const float MAX_ATK_SPEED = 0.45f;
    private const float MIN_ATK_SPEED = 2f;
    private float specialAttackTimer;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected virtual bool SpecialAttack(){

        return true;
    }
}
