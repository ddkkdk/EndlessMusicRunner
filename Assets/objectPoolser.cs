using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class objectPoolser : MonoBehaviour
{
    [System.Serializable]
    public class Pool 
    {
        public string tag;
        public GameObject prefab;
        public int size;
    
    }

    public static objectPoolser Instance;
    public float spawnOffset = 1.0f; // Distance between spawned objects
    private Vector3 nextSpawnPosition;

    private void Awake()
    {
        Instance= this;
        nextSpawnPosition = new Vector3(27.7f, -0.02f, 0);
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictonary;
    void Start()
    {
        poolDictonary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) 
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) 
            {
                GameObject platformObj = Instantiate(pool.prefab);
                platformObj.SetActive(false);
                objectPool.Enqueue(platformObj);


            }

            poolDictonary.Add(pool.tag, objectPool);
        
        }
        
    }

    public GameObject SpawnFromPool(string tag,  Quaternion rotation) 
    {
        if (!poolDictonary.ContainsKey(tag)) 
        {
            Debug.LogWarning("Pool with tag" + tag + "doesn't exist");
            return null;
        
        }

        GameObject objectToSpawn = poolDictonary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = nextSpawnPosition;
        objectToSpawn.transform.rotation = rotation;

       /* IPooledObject pooledObj=objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null) 
        {
            pooledObj.OnObjectSpawn();
        }*/

        poolDictonary[tag].Enqueue(objectToSpawn);
        nextSpawnPosition.x += spawnOffset;
        return objectToSpawn;
    
    }

    public void SpawnObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnFromPool("platform", Quaternion.identity);
        }
    }


}
