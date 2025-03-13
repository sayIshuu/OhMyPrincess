using UnityEngine;
using UnityEngine.UI;


public abstract class Unit : MonoBehaviour
{
    public Animator animator;
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
        animator = GetComponent<Animator>();
        //health = 100;
        //attackDamage = 10;
        //attackSpeed = 1;
        //attackRange = 1;
    }

    protected virtual void Update()
    {
        //�����̽��� ������ ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(null);
        }
    }

    public virtual void Attack(Enemy target)
    {
        //UiAnimationManager.Instance.PlayAnimation(unitImage, attackSprites);
        //animator.SetTrigger("doAttack");
        animator.SetTrigger("doHit");
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
