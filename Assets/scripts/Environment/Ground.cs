using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Vector3 newstartPosition;
    public float lastPosition;

    public float speed;
  

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (transform.position.x < lastPosition)
        {
           // transform.position = newstartPosition;
            Destroy(gameObject);

        }



    }
}
