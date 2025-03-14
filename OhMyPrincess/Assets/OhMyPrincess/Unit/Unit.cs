using UnityEngine;
using UnityEngine.UI;


public abstract class Unit : MonoBehaviour
{
    protected Animator animator;
    //밸런스 조정 끝나면 protected로 변경.
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
        animator = GetComponent<Animator>();
        //health = 100;
        //attackDamage = 10;
        //attackSpeed = 1;
        //attackRange = 1;
    }

    protected virtual void Update()
    {
        
    }

    public virtual void Attack(Enemy target)
    {
        //UiAnimationManager.Instance.PlayAnimation(unitImage, attackSprites);
        animator.SetTrigger("doAttack");
    }

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
        ObjectPoolManager.Instance.ReturnUnitObject(unitType, gameObject);
    }
}
