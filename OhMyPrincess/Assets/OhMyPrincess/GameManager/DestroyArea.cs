using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(nameof(TagType.Enemy)))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            ObjectPoolManager.Instance.ReturnEnemyObject(enemy.enemyType, collision.gameObject);
        }
        else if (collision.gameObject.CompareTag(nameof(TagType.Skill)))
        {
            Skill skill = collision.gameObject.GetComponent<Skill>();
            ObjectPoolManager.Instance.ReturnSkillObject(skill.skillType, collision.gameObject);
        }
    }
}
