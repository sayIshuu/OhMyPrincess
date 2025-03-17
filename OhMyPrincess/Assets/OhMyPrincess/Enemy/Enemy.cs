using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    [Header("Enemy Stats")]
    public EnemyType enemyType;
    public float health;
    public float attackDamage;
    public float attackSpeed;
    public float moveSpeed;
    public int reward;

    public bool isMoving = true;
    public bool isAttacking = false;
    public bool isDied = false;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        health += PrincessManager.Instance.princessStress / 10;
        attackDamage += PrincessManager.Instance.princessStress / 50;
    }

    private void FixedUpdate()
    {
        if (!isAttacking && isMoving)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(nameof(TagType.Unit)))
        {
            if(collision.gameObject.GetComponent<Unit>().unitType == UnitType.Batch)
            {
                return;
            }
            isAttacking = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            StartCoroutine(AttackCoroutine(collision.gameObject.GetComponent<Unit>()));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(nameof(TagType.Unit)) || collision.gameObject.CompareTag(nameof(TagType.Betrator)))
        {
            isAttacking = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private IEnumerator AttackCoroutine(Unit target)
    {
        while (isAttacking)
        {
            Attack(target);
            yield return new WaitForSeconds(2.0f / attackSpeed);
        }
    }

    public virtual void Move()
    {
        // 왼쪽으로 이동 MovePosition사용
        Vector2 newPosition = rb.position + Vector2.left * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    public virtual void Attack(Unit target)
    {
        target.TakeDamage(attackDamage);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            StartCoroutine(DieCoroutine());
        }
    }

    public virtual bool TakeDamageBySkill(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            StartCoroutine(DieCoroutine());
            return true;
        }
        return false;
    }

    private IEnumerator DieCoroutine()
    {
        isMoving = false;
        if (isDied)
        {
            yield break;
        }
        isDied = true;
        animator.SetBool("Dead", true);
        GoldManager.Instance.AddGold(reward);
        yield return new WaitForSeconds(1.0f);
        Die();
    }

    protected virtual void Die()
    {
        animator.SetBool("Dead", false);
        isDied = false;
        health = 100;
        isMoving = true;
        ObjectPoolManager.Instance.ReturnEnemyObject(enemyType, gameObject);
    }
}
