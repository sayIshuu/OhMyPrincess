using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    protected Animator animator;
    private UnitDraggable unitDraggable;
    //밸런스 조정 끝나면 protected로 변경.
    [Header("Unit Stats")]
    public UnitType unitType;
    public float health;
    public float attackDamage;
    public float attackSpeed;
    private bool isAttacking;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        isAttacking = false;
        unitDraggable = GetComponent<UnitDraggable>();
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
            animator.SetTrigger("doAttack");
            Attack(target);
            yield return new WaitForSeconds(2.0f / attackSpeed);
        }
    }

    public virtual void Attack(Enemy target)
    {
        animator.SetTrigger("doAttack");
        target.TakeDamage(attackDamage);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("doHit");
        if (health <= 0)
        {
            DieCoroutine();
        }
    }

    private IEnumerator DieCoroutine()
    {
        animator.SetTrigger("doDie");
        yield return new WaitForSeconds(2.0f);
        Die();
    }

    //우선은 protected로 선언했지만, 스트레스로 인한 외부 사망요인 추가시 public으로 변경해줘야할 것.
    protected virtual void Die()
    {
        unitDraggable.UnitDied();
        ObjectPoolManager.Instance.ReturnUnitObject(unitType, gameObject);
    }
}
