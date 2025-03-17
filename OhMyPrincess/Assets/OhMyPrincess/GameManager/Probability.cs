using UnityEngine;

public class Probability : MonoBehaviour
{
    public static bool ProbabilityCheck(float percent)
    {
        return Random.Range(0f, 100f) < percent;
    }
}
