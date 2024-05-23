using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public List<MonsterItem> monster;
    public float itemSpawnDelay = 1;
    public float setSpawnDelay = 2f;

    public GameObject spawnObjects;

    public Transform spawnPoint_1;
    public Transform spawnPoint_2;

    private bool isSpawning = false;
    public bool startSpawn = false;
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

    public void StartSpawningObjects(bool isSpawn) 
    {
        if(isSpawn)
            StartCoroutine(SpawnMonstersAtRandomPos());

    }

  
    IEnumerator SpawnMonstersAtRandomPos()
    {
        
        if (monster.Count == 0)
        {
            Debug.LogWarning("Item sets list is empty!");
            yield break;
        }


        while (true) 
        {
            for (int setIndex = 0; setIndex < monster.Count; setIndex++)
            {
                MonsterItem currentSet = monster[setIndex];
                Transform spawnPoint = (setIndex==1|| setIndex==5) ? spawnPoint_1 : spawnPoint_2;


                for (int i = 0; i < currentSet.monster.Length; i++)
                {
                    GameObject item = currentSet.monster[i];
                    if (item != null)
                    {
                       // Transform spawnPoint = (i == 1 || i == 5) ? spawnPoint_1 : spawnPoint_2;


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
