using UnityEngine;

public class Archer : Unit
{
    protected override void Start()
    {
        base.Start();
        unitType = UnitType.Archer;
        animator = GetComponent<Animator>();
        //health = 100;
        //attackDamage = 10;
        //attackSpeed = 1;
        //attackRange = 1;
    }

    protected override void Die()
    {
        base.Die();
        health = 100;
    }
}
