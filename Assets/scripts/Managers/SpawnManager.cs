using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public List<MonsterItem> monster;
    public float itemSpawnDelay;
    public float setSpawnDelay;

    public GameObject spawnObjects;
    public Transform spawnPoint_1;
    public Transform spawnPoint_3;

    private bool isSpawning = false;
    public bool startSpawn = false;

    private Transform spawnPoint;

    public GameObject bossPrefab;
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

        // itemSpawnDelay = Random.Range(0.1f, 0.5f);
        //setSpawnDelay = Random.Range(0.1f, 0.75f);


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
        while (true)
        {
            for (int setIndex = 0; setIndex < monster.Count; setIndex++)
            {
                MonsterItem currentSet = monster[0];


                if (!isBuildTestingRandomMonster)
                {
                    if (setIndex == 0 || setIndex == 1 || setIndex == 2 || setIndex == 5 || setIndex == 6
                   || setIndex == 7 || setIndex == 9 || setIndex == 10 || setIndex == 11 || setIndex == 12 || setIndex == 13)
                    {
                        spawnPoint = spawnPoint_1;


                    }
                    else if (setIndex == 3 || setIndex == 4 || setIndex == 8 || setIndex == 14 || setIndex == 15
                        || setIndex == 16 || setIndex == 17)
                    {
                        spawnPoint = spawnPoint_3;
                    }

                }
                for (int i = 0; i < currentSet.monster.Length; i++)
                {
                    GameObject item = currentSet.monster[i];
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
                //CreatBossCounting++;
                //if (CreatBossCounting >= CreatBossCountingDuration && !isCreatBoss)
                //{
                //    isCreatBoss = true;
                //    var obj = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);

                //}
                yield return new WaitForSeconds(setSpawnDelay);
            }

            print("생성 완료");
        }



    }

}
