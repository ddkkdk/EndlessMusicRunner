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
    public GameObject platform;

    public float platformSize;
    public GameObject plaformPoint;
    public float xSpawn = 0;

    public Vector3 offset;
 
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


        SpawnPlatform(50);
    }

   

    public void SpawnPlatform(int count) 
    {
        for (int i = 0; i <= 10; i++)
        {
            GameObject spawnedObjects = Instantiate(platform, transform.right * xSpawn + offset, plaformPoint.transform.rotation); ;
            platformSize = spawnedObjects.GetComponent<BoxCollider2D>().size.x;
            Debug.Log("platsize" + platformSize);
            
            xSpawn += platformSize;

        }

    }
}
