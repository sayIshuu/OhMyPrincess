using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected Rigidbody2D rb;
    public SkillType skillType;
    public float damage;
    public float speed;
    public float mentalCost;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(nameof(TagType.Enemy)))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public virtual void Move()
    {
        Vector2 newPosition = rb.position + Vector2.right * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
