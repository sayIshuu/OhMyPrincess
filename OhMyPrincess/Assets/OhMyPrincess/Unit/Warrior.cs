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
        //health = 150;
        //attackDamage = 15;
        //attackSpeed = 1;
        //attackRange = 1;
    }

    public override void TakeDamage(float damage)
    {
        //���¸�ŭ ������ ����. Mathf ����ؼ�
        float reducedDamage = Mathf.Max(damage - armor, 0);
        base.TakeDamage(damage);
    }
}
