using UnityEngine;

public class Archer : Unit
{
    protected override void Start()
    {
        base.Start();
        unitType = UnitType.Archer;
        //health = 100;
        //attackDamage = 10;
        //attackSpeed = 1;
        //attackRange = 1;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
