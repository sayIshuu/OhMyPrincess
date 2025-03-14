using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

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

    public int gold = 1000;
    public TextMeshProUGUI goldText;


    private void Start()
    {
        UpdateGoldUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }

    public bool checkGold(int amount)
    {
        if (gold >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpendGold(int amount)
    {
        gold -= amount;
        UpdateGoldUI();
    }

    private void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = gold.ToString() + "G";
        }
    }
}
