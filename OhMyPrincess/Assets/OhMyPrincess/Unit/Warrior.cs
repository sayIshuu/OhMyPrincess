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
        animator = GetComponent<Animator>();
    }

    public override void Attack(Enemy target)
    {
        base.Attack(target);
    }

    public override void TakeDamage(float damage)
    {
        //방어력만큼 데미지 감소. Mathf 사용해서
        float reducedDamage = Mathf.Max(damage - armor, 0);
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        health = 100;
    }

    public override void Burn()
    {
        base.Burn();
        float percent = 10 + (PrincessManager.Instance.princessStress / 5);
        if (Probability.ProbabilityCheck(percent))
        {
            PrincessManager.Instance.IncreaseStress(30);
        }
    }
}
