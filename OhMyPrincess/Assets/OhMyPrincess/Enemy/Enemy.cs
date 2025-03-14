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
    public float attackRange;
    public float moveSpeed;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        // 왼쪽으로 이동 MovePosition사용
        Vector2 newPosition = rb.position + Vector2.left * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(nameof(TagType.Unit)))
        {
            //collision.gameObject.GetComponent<Unit>().TakeDamage(attackDamage);
            Attack(collision.gameObject.GetComponent<Unit>());
        }
    }

    public virtual void Attack(Unit target)
    {
        target.TakeDamage(attackDamage);
    }

    public virtual void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        ObjectPoolManager.Instance.ReturnEnemyObject(enemyType, gameObject);
    }
}
