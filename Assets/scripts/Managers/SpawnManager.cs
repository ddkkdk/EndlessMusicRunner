using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public List<MonsterItem> monster;
    public float itemSpawnDelay ;
    public float setSpawnDelay ;

    public GameObject spawnObjects;
    public Transform spawnPoint_1;
    public Transform spawnPoint_3;

    private bool isSpawning = false;
    public bool startSpawn = false;

    private Transform spawnPoint;

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
        if(isSpawn)
            StartCoroutine(SpawnMonstersAtRandomPos());

    }


    public bool isBuildTestingRandomMonster = true;
    public int CreatBossCounting = 0;
    public readonly int CreatBossCountingDuration = 30;
    public bool isCreatBoss = false;
    IEnumerator SpawnMonstersAtRandomPos()
    {
        
        if (monster.Count == 0)
        {
            
            yield break;
        }


        while (true) 
        {
            for (int setIndex = 0; setIndex < monster.Count; setIndex++)
            {
                MonsterItem currentSet = monster[0];

               
                if(!isBuildTestingRandomMonster)
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
                    if (item != null)
                    {
                        if(isBuildTestingRandomMonster)
                        {
                            int random = Random.Range(0,2);
                            if(random ==0)
                            {
                                spawnPoint = spawnPoint_1;
                            }
                            else
                                spawnPoint = spawnPoint_3;
                        }

                        GameObject spawnedObjects = Instantiate(item, spawnPoint.position, item.transform.rotation);
                        spawnedObjects.transform.SetParent(spawnObjects.transform);
                        CreatBossCounting++;
                        Debug.Log($"몬스터 소환된 횟수 : {CreatBossCounting}");
                        yield return new WaitForSeconds(itemSpawnDelay);
                    }
                }
                if(CreatBossCounting >=CreatBossCountingDuration &&!isCreatBoss)
                {
                    isCreatBoss = true;
                    Boss.Create();
                }
                yield return new WaitForSeconds(setSpawnDelay);
            }

            print("생성 완료");
        }

        
       
    }

}
