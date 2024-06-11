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
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            Boss.Create();
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
    IEnumerator SpawnMonstersAtRandomPos()
    {

        if (monster.Count == 0)
        {

            yield break;
        }

        //if (!isCreatBoss)
        //{
        //    isCreatBoss = true;
        //    var obj = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);

        //}

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
        while (true && isTestTableSpawn)
        {
            var level = GameData.Data.LevelDesigin[StageInfo];
            int idx = 0;
            for(int i =0;i<level.Count;i++)
            {
                var monsterInfo = GameData.Data.MonsterTable[level[idx].MonsterInfo];
                for(int j = 0; j < level[idx].MonsterSpwanCount;++j)
                {
                    MonsterSpwan(monsterInfo, (MonsterSpwanPosition)level[idx].Spwan_Position);

                    yield return new WaitForSeconds(level[idx].CoolTime);
                }
                idx++;
            }
            var obj = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
            counting++;
            Debug.Log($"몬스터재시작 {counting}");
            if(counting ==2)
            {
                yield break;
            }
        }

    }
    public int StageInfo = 0;
    public void MonsterSpwan(C_MonsterTable t, MonsterSpwanPosition spwanPosition)
    {
        //var t = GameData.Data.MonsterTable[idx];
        string prefab = string.Empty;
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
        Monster.Create("Monster", prefab, MySpwanPoint, t.MaxHp, t.Speed, t.Uniq_MonsterType);
    }
}

