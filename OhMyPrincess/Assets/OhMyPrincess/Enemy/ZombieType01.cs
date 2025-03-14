using UnityEngine;

public class ZombieType01 : Enemy
{
    protected override void Start()
    {
        base.Start();
        enemyType = EnemyType.ZombieType01;
        //health = 100;
        //attackDamage = 10;
        //attackSpeed = 1;
        //attackRange = 1;
        //moveSpeed = 10;
    }
}
