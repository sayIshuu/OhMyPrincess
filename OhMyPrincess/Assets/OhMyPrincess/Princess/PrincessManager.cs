using System.IO;
using UnityEngine;

public class PrincessManager : MonoBehaviour
{
    public static PrincessManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Animator animator;
    [Range(0, 100)]
    public float princessStress = 0;
    [SerializeField] private SpriteRenderer brigtness;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject princessStressGraph;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        princessStressGraph.transform.localScale = new Vector3(1000,20,1);
    }

    public void StartDragSkill()
    {
        animator.SetBool("dragSkill", true);
    }

    public void EndDragSkill()
    {
        animator.SetBool("dragSkill", false);
    }

    public void IncreaseStress(float amount)
    {
        princessStress = Mathf.Clamp(princessStress + amount, 0, 100);
        UpdatePrincessColorByStress();
    }

    // ��Ʈ���� ����
    public void DecreaseStress(float amount)
    {
        princessStress = Mathf.Clamp(princessStress - amount, 0, 100);
        UpdatePrincessColorByStress();
    }

    private void UpdatePrincessColorByStress()
    {
        //brigtness�� ���� ����
        float brigtnessAlphaValue = Mathf.Clamp01(princessStress / 150f);
        brigtness.color = new Color(0, 0, 0, brigtnessAlphaValue); // ���� ����
        float colorValue = Mathf.Clamp01(1 - (princessStress / 100f)); // 0~1 ������ ��ȯ
        spriteRenderer.color = new Color(colorValue, colorValue, colorValue); // Grayscale ����

        float graphScaleX = 1000 - princessStress * 10;
        princessStressGraph.transform.localScale = new Vector3(graphScaleX, 20, 1);
    }
}
