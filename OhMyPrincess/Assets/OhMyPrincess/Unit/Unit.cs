using UnityEngine;

public enum UnitType
{
    Warrior,
    Archer
}

public abstract class Unit : MonoBehaviour
{
    //밸런스 조정 끝나면 protected로 변경.
    [Header("Unit Stats")]
    public UnitType unitType;
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

    //우선은 protected로 선언했지만, 스트레스로 인한 외부 사망요인 추가시 public으로 변경해줘야할 것.
    protected virtual void Die()
    {
        //Destroy(gameObject);

        //오브젝트 풀링으로 최적화
        ObjectPoolManager.Instance.ReturnUnitObject(unitType, gameObject);
    }
}
