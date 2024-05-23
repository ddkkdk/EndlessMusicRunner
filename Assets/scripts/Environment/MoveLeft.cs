using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
   
    void Update()
    {
        transform.Translate(Vector2.left * speed *Time.deltaTime);

        if (transform.position.x < -60) 
        {
            Destroy(this.gameObject);
        }
        
    }
}
