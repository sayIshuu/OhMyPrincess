using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    [SerializeField] private Animator princessAnimator;

    //적 부딫히면 ㅇㅇ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(nameof(TagType.Enemy)) )
        {
            princessAnimator.SetTrigger("doHit");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            ObjectPoolManager.Instance.ReturnEnemyObject(enemy.enemyType, collision.gameObject);
            HpManager.Instance.DecreaseHp();
        }
        else if(collision.gameObject.CompareTag(nameof(TagType.Betrator)))
        {
            princessAnimator.SetTrigger("doHit");
            Unit unit = collision.gameObject.GetComponent<Unit>();
            ObjectPoolManager.Instance.ReturnUnitObject(unit.unitType, collision.gameObject);
            HpManager.Instance.DecreaseHp();
        }
    }
}
