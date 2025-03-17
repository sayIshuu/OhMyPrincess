using UnityEngine;

public class Archer : Unit
{
    protected override void Start()
    {
        base.Start();
        unitType = UnitType.Archer;
        animator = GetComponent<Animator>();
    }

    protected override void Die()
    {
        base.Die();
        health = 100;
    }

    public override void Burn()
    {
        base.Burn();
        //공주스트레스가 높을수록 확률이 올라감.
        float percent = 10 + (PrincessManager.Instance.princessStress / 5);
        if (Probability.ProbabilityCheck(percent))
        {
            PrincessManager.Instance.IncreaseStress(35);
        }
    }
}
