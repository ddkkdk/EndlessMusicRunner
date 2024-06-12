using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public List<MonsterItem> monster;
    public float itemSpawnDelay;
    public float setSpawnDelay;

    public GameObject spawnObjects;
    public Transform spawnPoint_1;
    public Transform spwanPoint_2;
    public Transform spawnPoint_3;
    public Transform bossSpawnPoint;

    private bool isSpawning = false;
    public bool startSpawn = false;

    private Transform spawnPoint;

    public GameObject bossPrefab;


    public List<GameObject> monsterOBjects = new List<GameObject>();
    public List<GameObject> bossObjects = new List<GameObject>();
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);

        }
        else
        {
            instance = this;

        }

    }

    private void Update()
    {
        if (isCompletedSpawn )
        {
            gameOverTime += Time.deltaTime;

            if(gameOverTime >=2.5f)
            {
                AudioManager.instance.StopMusic();
                UI_GameOver.Create();
                isCompletedSpawn = false;
            }    
        }
    }

    public void StartSpawningObjects(bool isSpawn)
    {
        if (isSpawn)
            StartCoroutine(SpawnMonstersAtRandomPos());

    }


    public bool isBuildTestingRandomMonster = true;
    public int CreatBossCounting = 0;
    public readonly int CreatBossCountingDuration = 2;
    public bool isCreatBoss = false;

    public bool isTestTableSpawn = false;
    bool isCompletedSpawn = false;
    float gameOverTime = 0f;
    IEnumerator SpawnMonstersAtRandomPos()
    {


        #region Old MonsterSpwan
        while (true && !isTestTableSpawn)
        {
            for (int setIndex = 0; setIndex < monster.Count; setIndex++)
            {
                MonsterItem currentSet = monster[0];
                for (int i = 0; i < currentSet.monster.Length; i++)
                {
                    GameObject item = currentSet.monster[i];
                    //print(item.name);
                    int itemNumber = item.GetComponent<MoveLeft>().monsterNumber;

                    if (item != null)
                    {
                        if (isBuildTestingRandomMonster)
                        {
                            int random = Random.Range(0, currentSet.monster.Length);
                            if (random == 0 && itemNumber == 0 || itemNumber == 1 || itemNumber == 2 || itemNumber == 5 || itemNumber == 6
                                   || itemNumber == 7 || itemNumber == 9 || itemNumber == 10 || itemNumber == 11
                                   || itemNumber == 12 || itemNumber == 13 || itemNumber == 16)
                            {

                                spawnPoint = spawnPoint_1;
                            }
                            else
                                spawnPoint = spawnPoint_3;


                        }
                        if (i == 3)
                            spawnPoint = spawnPoint_3;

                        GameObject spawnedObjects = Instantiate(item, spawnPoint.position, item.transform.rotation);
                        spawnedObjects.transform.SetParent(spawnObjects.transform);

                        yield return new WaitForSeconds(itemSpawnDelay);
                    }
                }
                CreatBossCounting++;
                if (CreatBossCounting >= CreatBossCountingDuration && !isCreatBoss)
                {
                    isCreatBoss = true;
                    var obj = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);

                }
                yield return new WaitForSeconds(setSpawnDelay);
            }

            print("생성 완료");
        }
        #endregion
        Debug.Log("몬스터 시작");
        int counting = 0;
        bool isNewCreatBoss = false; // 보스생성했는가에 대한 변수
        while (true && isTestTableSpawn)
        {
            var level = GameData.Data.LevelDesigin[StageInfo];
            int idx = 0;
            for(int i =0;i<level.Count;i++)
            {
                var monsterInfo = GameData.Data.MonsterTable[level[idx].MonsterInfo];

                if (level[idx].MonsterInfo / 1000 ==1 )
                {
                    for (int j = 0; j < level[idx].MonsterSpwanCount; ++j)
                    {
                        LongNoteSpawn(monsterInfo, (MonsterSpwanPosition)level[idx].Spwan_Position);

                        yield return new WaitForSeconds(level[idx].CoolTime);
                    }
                }
                else
                {
                    for (int j = 0; j < level[idx].MonsterSpwanCount; ++j)
                    {
                        //보스 한번 생성했는지검사
                        bool isBoss = monsterInfo.monsterType == Monster_Type.Boss;
                        //보스 한번생성한적이 있다면 보스생성하지않고 돌아오는형태
                        if (isBoss && isNewCreatBoss) continue;

                        MonsterSpawn(monsterInfo, (MonsterSpwanPosition)level[idx].Spwan_Position);

                        if (isBoss)
                            isNewCreatBoss = true;

                        yield return new WaitForSeconds(level[idx].CoolTime);
                    }
                }
               
                idx++;
            }
            var bossPosition = Vector3.zero;
            bossPosition.x = -40;
            
            counting++;
            Debug.Log($"몬스터재시작 {counting}");
            if(counting ==2)
            {
                isCompletedSpawn = true;
                Debug.Log($"최종 몬스터 수 :{LevelDesignMonsterCount.GetMonsterCount(0)}");
                yield break;
            }
        }

    }
    public int StageInfo = 0;
    public void MonsterSpawn(C_MonsterTable t, MonsterSpwanPosition spwanPosition,bool CreatBoss = false)
    {
        //var t = GameData.Data.MonsterTable[idx];
        string prefab = string.Empty;
        bool isMonster = t.monsterType == Monster_Type.Normal;
        if (t.monsterType == Monster_Type.Normal)
            prefab = monsterOBjects[t.PrefabName].name;
        else if (t.monsterType == Monster_Type.Boss)
            prefab = bossObjects[t.PrefabName].name;
        
        if(t.Uniq_MonsterType == UniqMonster.SendBack)
        {
            Monster.Create("Monster", prefab, spwanPoint_2, t.MaxHp, t.Speed, t.Uniq_MonsterType);
            return;
        }
        var MySpwanPoint = spawnPoint_1;
        switch(spwanPosition)
        {
            case MonsterSpwanPosition.Down:
                MySpwanPoint = spawnPoint_3;
                break;
            case MonsterSpwanPosition.Random:
                int random = Random.Range(0, 2);
                if (random == 1)
                    MySpwanPoint = spawnPoint_3;
                break;
        }
        if(isMonster)
            Monster.Create("Monster", prefab, MySpwanPoint, t.MaxHp, t.Speed, t.Uniq_MonsterType);
        else if(!isMonster)
        {
            Boss.Create(bossSpawnPoint.position);
        }
    }

    public void LongNoteSpawn(C_MonsterTable t, MonsterSpwanPosition spwanPosition)
    {
        string prefab = string.Empty;
        prefab = monsterOBjects[t.PrefabName].name;

        var MySpwanPoint = spawnPoint_1;
        switch (spwanPosition)
        {
            case MonsterSpwanPosition.Down:
                MySpwanPoint = spawnPoint_3;
                break;
            case MonsterSpwanPosition.Random:
                int random = Random.Range(0, 2);
                if (random == 1)
                    MySpwanPoint = spawnPoint_3;
                break;
        }
        LongNote.Create("Monster", prefab, MySpwanPoint,t.Speed);
    }
}

