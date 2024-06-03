using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskRotator : MonoBehaviour
{
    public GameObject charecterDisk;
    public void RotateDisk() 
    {
        charecterDisk.transform.Rotate(0,0,33);
    
    }
}
