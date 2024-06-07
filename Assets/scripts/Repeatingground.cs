using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeatingground : MonoBehaviour
{
    public float startPosition;
    public float lastPosition;

    public float speed;


    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (transform.position.x <= lastPosition)
        {
            Vector2 pos = new Vector2(startPosition, transform.position.y);
            transform.position = pos;
            // Destroy(gameObject);

        }





    }
}