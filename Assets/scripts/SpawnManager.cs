using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject monster;
    public float startDelay = 2;
    public float repeatDelay = 1.2f;
    private Vector2 spawnPosition = new Vector2(-21.7f, 1.05f);
    void Start()
    {

        InvokeRepeating("SpawnMonsters", startDelay, repeatDelay);
    }

    void SpawnMonsters() 
    {
        Instantiate(monster, spawnPosition, monster.transform.rotation);

    }


}
