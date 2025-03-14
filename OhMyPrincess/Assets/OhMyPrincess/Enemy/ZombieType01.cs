using UnityEngine;

public class ZombieType01 : Enemy
{
    protected override void Start()
    {
        base.Start();
        enemyType = EnemyType.ZombieType01;
    }
}
