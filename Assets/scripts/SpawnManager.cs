using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<MonsterItem> monster;
    public float startDelay = 2;
    public float repeatDelay = 1.2f;

    public float spawnInterval = 3f;
    private float timer;

    public Transform spawnPoint_1;
    public Transform spawnPoint_2;
    void Start()
    {

        //InvokeRepeating("SpawnMonstersAtRandomPos", startDelay, repeatDelay);
        timer = spawnInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0) 
        {
            SpawnMonstersAtRandomPos();
            timer = spawnInterval;


        }
    }

    void SpawnMonstersAtRandomPos()
    {
        
        if (monster.Count == 0)
        {
            Debug.LogWarning("Item sets list is empty!");
            return;
        }

        
        int randomSetIndex = Random.Range(0, monster.Count);
        MonsterItem selectedSet = monster[randomSetIndex];

        Transform spawnPoint=Random.value > 0.5f ? spawnPoint_1 : spawnPoint_2;

        
        foreach (GameObject item in selectedSet.monster)
        {
            if (item != null)
            {
                Instantiate(item, spawnPoint.position, item.transform.rotation);
            }
        }
    }

}
