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

    public bool isAttacking = false;
    public bool isDied = false;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(nameof(TagType.Unit)))
        {
            isAttacking = true;
            StartCoroutine(AttackCoroutine(collision.gameObject.GetComponent<Unit>()));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(nameof(TagType.Unit)))
        {
            isAttacking = false;
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
        if(health > 0)
        {
            animator.SetTrigger("Hit");
            health -= damage;
        }
        else
        {
            StartCoroutine(DieCoroutine());
        }
    }

    private IEnumerator DieCoroutine()
    {
        if(isDied)
        {
            yield break;
        }
        isDied = true;
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.0f);
        Die();
    }

    protected virtual void Die()
    {
        animator.SetBool("Dead", false);
        isDied = false;
        health = 100;
        ObjectPoolManager.Instance.ReturnEnemyObject(enemyType, gameObject);
    }
}
