using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    //싱글톤 패턴을 위한 인스턴스 변수
    public static ObjectPoolManager Instance;

    //프리팹들
    [Header("Unit Prefabs")]
    public GameObject warriorObject;
    public GameObject archerObject;
    private int unitPoolSize = 30;

    [Header("Enemy Prefabs")]
    public GameObject zombieType01Object;
    private int enemyPoolSize = 30;

    [Header("Skill Prefabs")]
    public GameObject fireBallObject;
    private int skillPoolSize = 10;

    [Header("Item Prefabs")]
    public GameObject batchObject;
    private int itemPoolSize = 3;

    // 큐를 사용하는게 직관적인듯.
    private Queue<GameObject> warriorPool = new Queue<GameObject>();
    private Queue<GameObject> archerPool = new Queue<GameObject>();
    private Queue<GameObject> zombieType01Pool = new Queue<GameObject>();
    private Queue<GameObject> fireBallPool = new Queue<GameObject>();
    private Queue<GameObject> batchPool = new Queue<GameObject>();

    //싱글톤
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

    //오브젝트 풀링에서 Start는 미리 생성하는 역할을 해준다.
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

        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject zombieType01 = Instantiate(zombieType01Object);
            zombieType01.SetActive(false);
            zombieType01Pool.Enqueue(zombieType01);
        }

        for (int i = 0; i < skillPoolSize; i++)
        {
            GameObject fireBall = Instantiate(fireBallObject);
            fireBall.SetActive(false);
            fireBallPool.Enqueue(fireBall);
        }

        for (int i = 0; i < itemPoolSize; i++)
        {
            GameObject batch = Instantiate(batchObject);
            batch.SetActive(false);
            batchPool.Enqueue(batch);
        }
    }

    //풀에서 유닛을 가져오는 함수. 부족한 경우는 없음. 격자가 최대 27인데 30개씩 생성하니까.
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
        else if (unitType == UnitType.Batch)
        {
            if (batchPool.Count > 0)
            {
                GameObject batch = batchPool.Dequeue();
                batch.SetActive(true);
                return batch;
            }
            else
            {
                GameObject batch = Instantiate(batchObject);
                return batch;
            }
        }
        else
        {
            return null;
        }
    }

    public GameObject GetEnemyObject(EnemyType enemyType)
    {
        if (enemyType == EnemyType.ZombieType01)
        {
            if (zombieType01Pool.Count > 0)
            {
                GameObject zombieType01 = zombieType01Pool.Dequeue();
                zombieType01.SetActive(true);
                return zombieType01;
            }
            else
            {
                GameObject zombieType01 = Instantiate(zombieType01Object);
                return zombieType01;
            }
        }
        else
        {
            return null;
        }
    }

    public GameObject GetSkillObject(SkillType skillType)
    {
        if (skillType == SkillType.FireBall)
        {
            if (fireBallPool.Count > 0)
            {
                GameObject fireBall = fireBallPool.Dequeue();
                fireBall.SetActive(true);
                return fireBall;
            }
            else
            {
                GameObject fireBall = Instantiate(fireBallObject);
                return fireBall;
            }
        }
        else
        {
            return null;
        }
    }

    //풀로 유닛을 반환하는 함수.
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
        else if (unitType == UnitType.Batch)
        {
            batchPool.Enqueue(unit);
        }
    }

    public void ReturnEnemyObject(EnemyType enemyType, GameObject enemy)
    {
        enemy.SetActive(false);
        if (enemyType == EnemyType.ZombieType01)
        {
            zombieType01Pool.Enqueue(enemy);
        }
    }

    public void ReturnSkillObject(SkillType skillType, GameObject skill)
    {
        skill.SetActive(false);
        if (skillType == SkillType.FireBall)
        {
            fireBallPool.Enqueue(skill);
        }
    }
}
