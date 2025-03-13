using UnityEngine;
using System;

public class Warrior : Unit
{
    //예를들어 전사는 추가 방어력을 가지고 있을 수 있음.
    public float armor;

    protected override void Start()
    {
        base.Start();
        // 스탯 재설정! 이거 때문에라도 상속 하는듯. 하나하나 어캐 넣어줘
        unitType = UnitType.Warrior;
        //health = 150;
        //attackDamage = 15;
        //attackSpeed = 1;
        //attackRange = 1;
    }

    public override void TakeDamage(float damage)
    {
        //방어력만큼 데미지 감소. Mathf 사용해서
        float reducedDamage = Mathf.Max(damage - armor, 0);
        base.TakeDamage(damage);
    }
}
