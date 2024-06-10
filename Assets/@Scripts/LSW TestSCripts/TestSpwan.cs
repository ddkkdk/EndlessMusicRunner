using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpwan : MonoBehaviour
{
    public GameObject sendBack;
    public GameObject Boss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            Instantiate(sendBack,Vector3.zero,Quaternion.identity);
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            Instantiate(Boss, Vector3.zero, Quaternion.identity);
        }
    }
}
