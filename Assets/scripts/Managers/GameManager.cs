using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SkeletonAnimation skeleton;
    public Sprite background;
   
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

        skeleton = GameObject.Find("Player").transform.GetChild(0).GetComponent<SkeletonAnimation>();
    }

    // After playing some times background will
    // will change autometically .
    // Implement it here

    public void AnimationController(string animationName) 
    {
        skeleton.AnimationState.SetAnimation(0, animationName, false);
        

    }
}
