using UnityEngine;

public class UnitStress : MonoBehaviour
{
    [Range(0, 100)]
    public float stress;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Unit unit;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        unit = GetComponent<Unit>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void UpdateUnitColorByStress()
    {
        float colorValue = Mathf.Clamp01(1 - (stress / 100f)); // 0~1 범위로 변환
        spriteRenderer.color = new Color(colorValue, colorValue, colorValue); // Grayscale 적용
    }

    // 스트레스 증가
    public void IncreaseStress(float amount)
    {
        stress = Mathf.Clamp(stress + amount, 0, 100);
        UpdateUnitColorByStress();
        if (stress == 100)
        {
            Collapse();
        }
    }

    // 스트레스 감소
    public void DecreaseStress(float amount)
    {
        stress = Mathf.Clamp(stress - amount, 0, 100);
        UpdateUnitColorByStress();
    }

    private void Collapse()
    {
        unit.isCollapsed = true;
        unit.gameObject.tag = nameof(TagType.Enemy);
        spriteRenderer.flipX = true;
        unit.isAttacking = false;
        unit.gameObject.layer = LayerMask.NameToLayer("enemy");

        //X position freeze 해제하기
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        StopAllCoroutines();
    }
}
