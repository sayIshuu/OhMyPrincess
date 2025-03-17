using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    protected Animator animator;
    private UnitDraggable unitDraggable;
    private UnitStress unitStress;
    private Rigidbody2D rb;
    //밸런스 조정 끝나면 protected로 변경.
    [Header("Unit Stats")]
    public UnitType unitType;
    public float health;
    public float attackDamage;
    public float attackSpeed;
    public float moveSpeed;

    public bool isCollapsed;

    public int cost;
    public float energy;

    public bool isAttacking;
    private bool isDied;

    protected virtual void Start()
    {
        //animator = GetComponent<Animator>();
        isAttacking = false;
        isDied = false;
        isCollapsed = false;
        unitDraggable = GetComponent<UnitDraggable>();
        unitStress = GetComponent<UnitStress>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isAttacking && isCollapsed)
        {
            Move();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCollapsed)
        {
            if (collision.gameObject.CompareTag(nameof(TagType.Unit)))
            {
                isAttacking = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(AttackCoroutine(collision.gameObject.GetComponent<Unit>()));
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                isAttacking = true;
                StartCoroutine(AttackCoroutine(collision.gameObject.GetComponent<Enemy>()));
            }
            else if(collision.gameObject.CompareTag(nameof(TagType.Betrator)))
            {
                isAttacking = true;
                StartCoroutine(AttackCoroutine(collision.gameObject.GetComponent<Unit>()));
            }
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(nameof(TagType.Unit)) || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag(nameof(TagType.Betrator)))
        {
            isAttacking = false;
            if(isCollapsed)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
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

    private IEnumerator AttackCoroutine(Unit target)
    {
        while (isAttacking)
        {
            Attack(target);
            yield return new WaitForSeconds(2.0f / attackSpeed);
        }
        isAttacking = false;
    }

    public virtual void Attack(Enemy target)
    {
        if(health <= 0)
        {
            return;
        }
        animator.SetTrigger("doAttack");
        target.TakeDamage(attackDamage);
    }

    public virtual void Attack(Unit target)
    {
        if (health <= 0)
        {
            return;
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
            if (Probability.ProbabilityCheck(50))
            {
                unitStress.IncreaseStress(25);
            }
        }
        else
        {
            StartCoroutine(DieCoroutine());
        }
    }

    public virtual void Move()
    {
        if (isCollapsed)
        {
            Vector2 newPosition = rb.position + Vector2.left * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
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
        ObjectPoolManager.Instance.ReturnUnitObject(unitType, gameObject);
    }

    public virtual void Burn()
    {
        PrincessManager.Instance.DecreaseStress(energy);
        Die();
    }
}