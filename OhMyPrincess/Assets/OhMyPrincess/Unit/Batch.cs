using UnityEngine;

public class Batch : Unit
{
    protected override void Start()
    {
        base.Start();
        unitType = UnitType.Batch;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        return;
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        return;
    }
}
