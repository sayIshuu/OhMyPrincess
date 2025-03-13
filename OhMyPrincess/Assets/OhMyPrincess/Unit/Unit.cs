using UnityEngine;
using UnityEngine.UI;


public abstract class Unit : MonoBehaviour
{
    //�뷱�� ���� ������ protected�� ����.
    [Header("Unit Stats")]
    public UnitType unitType;
    public float health;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    //[Header("Attack")]
    //public Sprite[] attackSprites;

    //private Image unitImage;

    protected virtual void Start()
    {
        //health = 100;
        //attackDamage = 10;
        //attackSpeed = 1;
        //attackRange = 1;
    }

    public virtual void Attack(Enemy target)
    {
        //UiAnimationManager.Instance.PlayAnimation(unitImage, attackSprites);
    }

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
        ObjectPoolManager.Instance.ReturnUnitObject(unitType, gameObject);
    }
}
