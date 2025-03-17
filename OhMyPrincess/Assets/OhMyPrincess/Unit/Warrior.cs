using UnityEngine;
using System;

public class Warrior : Unit
{
    //������� ����� �߰� ������ ������ ���� �� ����.
    public float armor;

    protected override void Start()
    {
        base.Start();
        // ���� �缳��! �̰� �������� ��� �ϴµ�. �ϳ��ϳ� ��ĳ �־���
        unitType = UnitType.Warrior;
        animator = GetComponent<Animator>();
    }

    public override void Attack(Enemy target)
    {
        base.Attack(target);
    }

    public override void TakeDamage(float damage)
    {
        //���¸�ŭ ������ ����. Mathf ����ؼ�
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
