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
    public Transform spawnPoint_2;
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

        itemSpawnDelay = Random.Range(0.1f, 0.5f);
        setSpawnDelay = Random.Range(0.1f, 0.75f);

       
    }

    private void Update()
    {
        
    }

    public void StartSpawningObjects(bool isSpawn) 
    {
        if(isSpawn)
            StartCoroutine(SpawnMonstersAtRandomPos());

    }

  
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
                MonsterItem currentSet = monster[setIndex];

               
                if (setIndex == 1 || setIndex == 5)
                {
                     spawnPoint = spawnPoint_1;


                }
                else if (setIndex == 7 || setIndex == 8 || setIndex == 9|| setIndex == 10 || setIndex == 11 || setIndex == 12 || setIndex == 13
                    || setIndex == 14 || setIndex == 15 || setIndex == 16 || setIndex == 17) 
                {
                     spawnPoint = spawnPoint_3;
                }
                else 
                {
                     spawnPoint = spawnPoint_2;
                }

                for (int i = 0; i < currentSet.monster.Length; i++)
                {
                    GameObject item = currentSet.monster[i];
                    if (item != null)
                    {
                       

                        GameObject spawnedObjects =Instantiate(item, spawnPoint.position, item.transform.rotation) ;
                        spawnedObjects.transform.SetParent(spawnObjects.transform);

                        yield return new WaitForSeconds(itemSpawnDelay);
                    }
                }

                yield return new WaitForSeconds(setSpawnDelay);
            }


        }

        
       
    }

}
