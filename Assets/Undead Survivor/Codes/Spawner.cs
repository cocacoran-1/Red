using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    public enum SpawnLocation { Top_Point, Bottom_Point, Right_Point, Left_Point, Center_Point, All }

    public Dictionary<SpawnLocation, Transform[]> spawnLocations;
    public SpawnData[] spawnData;
    float timer;
    public int level;
    public int BossLevel;
    public int SlimeCount;  //총 보스 슬라임 수

    Dictionary<EnemyData, float> enemyTimers;
    public BossData bossData; // 보스 데이터를 추가합니다.

    public float bossTimer; // 보스 타이머를 추가합니다.
    public float bossSpawnInterval = 300f; // 보스 스폰 간격을 설정합니다 (5분 = 300초).
    public bool isBoss = false;
    public int currentBossLevel;
    public int randomIndex;

    public SpawnerManager spawnerManager;
    CanvasManager canvasManager;
    PoolManager poolManager;
    GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canvasManager = GameObject.Find("GamePlayCanvas").GetComponent<CanvasManager>();
        poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();

        spawnLocations = new Dictionary<SpawnLocation, Transform[]>();
        enemyTimers = new Dictionary<EnemyData, float>();
        foreach (SpawnLocation location in System.Enum.GetValues(typeof(SpawnLocation)))
        {
            if (location == SpawnLocation.All) continue;

            Transform spawnParent = transform.Find(location.ToString());
            if (spawnParent != null)
            {
                Transform[] points = spawnParent.GetComponentsInChildren<Transform>();
                spawnLocations.Add(location, points);
            }
            else
            {
                Debug.LogError("Spawn location not found!");
            }
        }
    }

    void Update()
    {
        if (!gameManager.isLive)
        {
            return;
        }
        level = Mathf.Min(Mathf.FloorToInt(canvasManager.gameTime / 60f), spawnData.Length - 1);
        SpawnData currentData = spawnData[level];

        // 보스가 살아있지 않을 때 일반 몬스터 스폰

        foreach (EnemyData enemy in currentData.enemyData)
        {
            if (!enemyTimers.ContainsKey(enemy))
            {
                enemyTimers[enemy] = 0;
            }

            enemyTimers[enemy] += Time.deltaTime;

            if (enemyTimers[enemy] > enemy.spawnTime)
            {
                /*if (!isBoss)
                {
                    enemyTimers[enemy] = 0;
                    Spawn(enemy);
                }
                else if (BossLevel < level - 1)
                {
                    enemyTimers[enemy] = 0;
                    Spawn(enemy);
                }*/
                enemyTimers[enemy] = 0;
                Spawn(enemy);
            }
        }
       
        bossTimer = canvasManager.gameTime;
        if (bossTimer >= bossSpawnInterval)
        {
            bossTimer = 0;
            currentBossLevel = level; // 현재 보스 레벨을 저장
            spawnerManager.boss_level = currentBossLevel;
            SpawnBoss();
            if(bossSpawnInterval<900)
            {
                bossSpawnInterval += 300f;
            }
            else
            {
                bossSpawnInterval += 9999f;
            }
        }
    }

    void Spawn(EnemyData enemy)
    {
        List<SpawnLocation> targetLocations;

        if (enemy.location == SpawnLocation.All)
        {
            // 센터 포인트와 All 값을 제외한 모든 SpawnLocation 값들을 리스트에 추가합니다.
            targetLocations = new List<SpawnLocation>(System.Enum.GetValues(typeof(SpawnLocation)).Cast<SpawnLocation>().Where(e => e != SpawnLocation.All && e != SpawnLocation.Center_Point));
        }
        else
        {
            targetLocations = new List<SpawnLocation> { enemy.location };
        }
        foreach (SpawnLocation location in targetLocations)
        {

            if (spawnLocations.ContainsKey(location))
            {
                Transform[] points = spawnLocations[location];

                for (int i = 0; i < enemy.spawnCount; i++)
                {
                    GameObject enemyObject = poolManager.GetEnemy(enemy.spriteType);
                    enemyObject.transform.position = points[Random.Range(1, points.Length)].position;
                    enemyObject.GetComponent<Enemy>().Init(enemy);
                }
            }
        }
    }

    void SpawnBoss()
    {
        isBoss = true;
        BossLevel = level;
       
        if(BossLevel==5 || BossLevel == 4 || BossLevel == 6)
        {
            randomIndex = Random.Range(0, 3);// 랜덤 인덱스 생성
        }
        else if(BossLevel==10|| BossLevel == 9|| BossLevel == 11)
        {
            randomIndex = Random.Range(3, 6);// 랜덤 인덱스 생성
        }
        else
        {
            randomIndex = bossData.bossInfos.Length-1;// 랜덤 인덱스 생성
        }
        BossInfo selectedBossInfo = bossData.bossInfos[randomIndex]; // 선택된 보스 정보 가져오기
        if (selectedBossInfo.spriteType == 19|| selectedBossInfo.spriteType == 20|| selectedBossInfo.spriteType == 21)
        {
            for (int i = 0; i < 3; i++)
            {
                int index = 19 + i;
                GameObject bossObject = poolManager.GetEnemy(index);
                Transform[] points = spawnLocations[selectedBossInfo.location];
                bossObject.transform.position = points[i + 1].position;

                // 선택한 보스의 체력과 속도를 적용합니다.
                bossData.health = selectedBossInfo.health;
                bossData.speed = selectedBossInfo.speed;
                bossData.spriteType = selectedBossInfo.spriteType;
                bossData.location = selectedBossInfo.location;
                bossObject.GetComponent<Enemy>().Init(bossData);
            }

        }
        else
        {

            GameObject bossObject = poolManager.GetEnemy(selectedBossInfo.spriteType);
            Transform[] points = spawnLocations[selectedBossInfo.location];
            bossObject.transform.position = points[Random.Range(1, points.Length)].position;

            // 선택한 보스의 체력과 속도를 적용합니다.
            bossData.health = selectedBossInfo.health;
            bossData.speed = selectedBossInfo.speed;
            bossData.spriteType = selectedBossInfo.spriteType;
            bossData.location = selectedBossInfo.location;
            bossObject.GetComponent<Enemy>().Init(bossData);
        }

    }
}



[System.Serializable]
public class SpawnData
{
    public EnemyData[] enemyData;
}

[System.Serializable]
public class BossInfo
{
    public int spriteType;
    public int health;
    public float speed;
    public int spawnCount;
    public float spawnTime;
    public Spawner.SpawnLocation location;
}

[System.Serializable]
public class BossData : EnemyData
{
    public BossInfo[] bossInfos; // 각 보스의 정보를 저장할 배열
}

[System.Serializable]
public class EnemyData
{
    public int spriteType;
    public int health;
    public float speed;
    public int spawnCount;
    public float spawnTime;
    public Spawner.SpawnLocation location;
}

