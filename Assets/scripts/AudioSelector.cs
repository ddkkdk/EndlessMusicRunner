using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelector : MonoBehaviour
{
    public static AudioSelector instance;
    public int audioNumber;
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

    
}
