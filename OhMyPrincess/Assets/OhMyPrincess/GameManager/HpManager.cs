using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HpManager : MonoBehaviour
{
    public static HpManager Instance;

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

    public float hp;
    public TextMeshProUGUI hpText;

    private void Start()
    {
        hp = 5;
        hpText.text = hp.ToString();
    }

    public void DecreaseHp()
    {
        hp--;
        UpdateHpUi();
        if (hp <= 0)
        {
           //¾À Àç·Îµå
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateHpUi()
    {
        hpText.text = hp.ToString();
    }
}
