using UnityEngine;

public class FireBall : Skill
{
    protected override void Start()
    {
        base.Start();
        skillType = SkillType.FireBall;
    }

}
