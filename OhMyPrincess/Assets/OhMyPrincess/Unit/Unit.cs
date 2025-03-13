using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Unit Stats")]
    public float health;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;

    protected virtual void Start()
    {
        //health = 100;
        //attackDamage = 10;
        //attackSpeed = 1;
        //attackRange = 1;
    }


    //public virtual void Attack(Enemy target) { }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    //�켱�� protected�� ����������, ��Ʈ������ ���� �ܺ� ������� �߰��� public���� ����������� ��.
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
