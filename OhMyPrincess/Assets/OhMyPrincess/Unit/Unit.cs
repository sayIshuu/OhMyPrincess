using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    protected Animator animator;
    private UnitDraggable unitDraggable;
    private UnitStress unitStress;
    //밸런스 조정 끝나면 protected로 변경.
    [Header("Unit Stats")]
    public UnitType unitType;
    public float health;
    public float attackDamage;
    public float attackSpeed;

    public int cost;

    private bool isAttacking;
    private bool isDied;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        isAttacking = false;
        isDied = false;
        unitDraggable = GetComponent<UnitDraggable>();
        unitStress = GetComponent<UnitStress>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isAttacking = true;
            StartCoroutine(AttackCoroutine(collision.gameObject.GetComponent<Enemy>()));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isAttacking = false;
        }
    }

    private IEnumerator AttackCoroutine(Enemy target)
    {
        while (isAttacking)
        {
            Attack(target);
            yield return new WaitForSeconds(2.0f / attackSpeed);
        }
    }

    public virtual void Attack(Enemy target)
    {
        if(health <= 0)
        {
            return;
        }
        if(Probability.ProbabilityCheck(50))
        {
            unitStress.IncreaseStress(10);
        }
        animator.SetTrigger("doAttack");
        target.TakeDamage(attackDamage);
    }

    public virtual void TakeDamage(float damage)
    {
        if(health > 0)
        {
            health -= damage;
            animator.SetTrigger("doHit");
        }
        else
        {
            StartCoroutine(DieCoroutine());
        }
    }

    private IEnumerator DieCoroutine()
    {
        if (isDied)
        {
            yield break;
        }
        animator.SetTrigger("doDie");
        isDied = true;
        yield return new WaitForSeconds(2.0f);
        Die();
    }

    //우선은 protected로 선언했지만, 스트레스로 인한 외부 사망요인 추가시 public으로 변경해줘야할 것.
    protected virtual void Die()
    {
        unitDraggable.UnitDied();
        isDied = false;
        health = 100;
        ObjectPoolManager.Instance.ReturnUnitObject(unitType, gameObject);
    }
}