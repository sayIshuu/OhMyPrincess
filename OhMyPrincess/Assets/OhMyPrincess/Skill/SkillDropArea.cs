using UnityEngine;

public class SkillDropArea : MonoBehaviour
{
    public void SkillDrop(Skill skill)
    {
        if(skill != null)
        {
            if(skill.skillType == SkillType.FireBall)
            {
                GameObject fireBall = ObjectPoolManager.Instance.GetSkillObject(SkillType.FireBall);
                fireBall.transform.position = transform.position;
            }
        }
    }
}
