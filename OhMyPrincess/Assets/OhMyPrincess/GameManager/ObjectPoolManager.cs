using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    //�̱��� ������ ���� �ν��Ͻ� ����
    public static ObjectPoolManager Instance;

    //�����յ�
    [Header("Unit Prefabs")]
    public GameObject warriorObject;
    public GameObject archerObject;
    private int unitPoolSize = 30;


    // ť�� ����ϴ°� �������ε�.
    private Queue<GameObject> warriorPool = new Queue<GameObject>();
    private Queue<GameObject> archerPool = new Queue<GameObject>();

    //�̱���
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //������Ʈ Ǯ������ Start�� �̸� �����ϴ� ������ ���ش�.
    private void Start()
    {
        for (int i = 0; i < unitPoolSize; i++)
        {
            GameObject warrior = Instantiate(warriorObject);
            warrior.SetActive(false);
            warriorPool.Enqueue(warrior);
            GameObject archer = Instantiate(archerObject);
            archer.SetActive(false);
            archerPool.Enqueue(archer);
        }
    }

    //Ǯ���� ������ �������� �Լ�. ������ ���� ����. ���ڰ� �ִ� 27�ε� 30���� �����ϴϱ�.
    public GameObject GetUnitObject(UnitType unitType)
    {
        if(unitType == UnitType.Warrior)
        {
            if (warriorPool.Count > 0)
            {
                GameObject warrior = warriorPool.Dequeue();
                warrior.SetActive(true);
                return warrior;
            }
            else
            {
                GameObject warrior = Instantiate(warriorObject);
                return warrior;
            }
        }
        else if (unitType == UnitType.Archer)
        {
            if (archerPool.Count > 0)
            {
                GameObject archer = archerPool.Dequeue();
                archer.SetActive(true);
                return archer;
            }
            else
            {
                GameObject archer = Instantiate(archerObject);
                return archer;
            }
        }
        else
        {
            return null;
        }
    }

    //Ǯ�� ������ ��ȯ�ϴ� �Լ�.
    public void ReturnUnitObject(UnitType unitType, GameObject unit)
    {
        unit.SetActive(false);
        if (unitType == UnitType.Warrior)
        {
            warriorPool.Enqueue(unit);
        }
        else if (unitType == UnitType.Archer)
        {
            archerPool.Enqueue(unit);
        }
        /* �̰� ��� �� ������ ���������,,, �� �𸣰ڴ�. => enum���� �ذ�.
        if (unit.GetComponent<Unit>().unitType == "warrior")
        {
            warriorPool.Enqueue(unit);
        }
        else if (unit.GetComponent<Unit>().unitType == "archer")
        {
            archerPool.Enqueue(unit);
        }
        */
    }
}
