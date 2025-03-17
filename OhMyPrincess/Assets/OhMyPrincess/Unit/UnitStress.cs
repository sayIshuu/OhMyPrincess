using System.Collections;
using UnityEngine;

public class UnitStress : MonoBehaviour
{
    [Range(0, 100)]
    public float stress;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Unit unit;

    public bool isHealing = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        unit = GetComponent<Unit>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void UpdateUnitColorByStress()
    {
        float colorValue = Mathf.Clamp01(1 - (stress / 100f)); // 0~1 ������ ��ȯ
        spriteRenderer.color = new Color(colorValue, colorValue, colorValue); // Grayscale ����
    }

    // ��Ʈ���� ����
    public void IncreaseStress(float amount)
    {
        stress = Mathf.Clamp(stress + amount, 0, 100);
        UpdateUnitColorByStress();
        if (stress == 100)
        {
            Collapse();
        }
    }

    // ��Ʈ���� ����
    public void DecreaseStress(float amount)
    {
        stress = Mathf.Clamp(stress - amount, 0, 100);
        UpdateUnitColorByStress();
    }

    public void StartHeal()
    {
        isHealing = true;
        StartCoroutine(HealCoroutine());
    }

    private IEnumerator HealCoroutine()
    {
        Color originalColor = spriteRenderer.color;
        float duration = 0.3f;
        float elapsedTime = 0;
        while (isHealing)
        {
            while (elapsedTime < duration)
            {
                float greenValue = Mathf.Lerp(originalColor.g, 1, elapsedTime / duration);
                spriteRenderer.color = new Color(originalColor.r, greenValue, originalColor.b);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            DecreaseStress(2);
            originalColor = spriteRenderer.color;
            elapsedTime = 0;
            while (elapsedTime < duration)
            {
                float greenValue = Mathf.Lerp(1, originalColor.g, elapsedTime / duration);
                spriteRenderer.color = new Color(originalColor.r, greenValue, originalColor.b);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(2.5f);
        }
        spriteRenderer.color = originalColor;
    }


    private void Collapse()
    {
        float percent = 30 + (PrincessManager.Instance.princessStress / 5);
        if (Probability.ProbabilityCheck(percent))
        {
            PrincessManager.Instance.IncreaseStress(30);
        }

        unit.isCollapsed = true;
        unit.gameObject.tag = nameof(TagType.Betrator);
        spriteRenderer.flipX = true;
        //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        // �ݶ��̴��� offset�� �����Ͽ� �浹üũ ��ġ�� ����
        boxCollider.offset = new Vector2(0.17f, boxCollider.offset.y);

        unit.isAttacking = false;
        unit.gameObject.layer = LayerMask.NameToLayer("enemy");
        unit.attackSpeed = 2.5f;
        //X position freeze �����ϱ�
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        StopAllCoroutines();
    }
    
}
