using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteConnector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "HittingCollider") 
        {
            this.gameObject.transform.position = other.transform.position;
            
        
        }
    }
}
