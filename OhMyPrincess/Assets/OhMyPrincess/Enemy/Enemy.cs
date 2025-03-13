using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
}
