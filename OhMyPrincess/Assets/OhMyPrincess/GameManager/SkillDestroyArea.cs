using UnityEngine;

public class SkillDestroyArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(nameof(TagType.Skill)))
        {
            Skill skill = collision.gameObject.GetComponent<Skill>();
            ObjectPoolManager.Instance.ReturnSkillObject(skill.skillType, collision.gameObject);
        }
    }
}
