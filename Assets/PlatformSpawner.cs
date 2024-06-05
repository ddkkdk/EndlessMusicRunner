using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private void FixedUpdate()
    {
        //objectPoolser.Instance.SpawnObjects(5);
    }
    public static PlatformSpawner instance;  
    public float itemSpawnDelay;
    public float setSpawnDelay;
    public GameObject platform;
    public Transform spawnPoint;  
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

        StartCoroutine(SpawnMonstersAtRandomPos());

    }

    IEnumerator SpawnMonstersAtRandomPos()
    {

        while (true)
        {
                                      
             GameObject spawnedObjects = Instantiate(platform, new Vector3(27.7f,-0.02f,0), platform.transform.rotation);
           
             yield return new WaitForSeconds(itemSpawnDelay);
                                  
        }


    }
}
