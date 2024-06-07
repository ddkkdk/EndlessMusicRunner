using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SkeletonAnimation skeleton;
    public GameObject speaker_1;
    public GameObject speaker_2;
    
    public float flyTimeScale;
    public float retireTimeScale;
    public float kickTimeScale;
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            speaker_1.gameObject.SetActive(true);
            speaker_2.gameObject.SetActive(true);
            Invoke("DeactivatSpeaker", 5f);
        
        }
    }

    public void PlayerAllAnimation() 
    {
    
    }

    public void AnimationController(string animationName) 
    {
      TrackEntry kickTrackEntry=  skeleton.AnimationState.SetAnimation(0, animationName, true);

        if (animationName == "fly")
        {
            kickTrackEntry.TimeScale = flyTimeScale;

        }
        else if (animationName == "retire") 
        {
            kickTrackEntry.TimeScale = retireTimeScale;
        }
        else if (animationName == "Kick")
        {
            kickTrackEntry.TimeScale = kickTimeScale;
        }

        kickTrackEntry.Complete += (TrackEntry trackEntry) =>
        {
            skeleton.AnimationState.SetAnimation(0, "Running", true);

        };
        

    }

    public void DeactivatSpeaker() 
    {
        speaker_1.gameObject.SetActive(false);
        speaker_2.gameObject.SetActive(false);

    }

    [SerializeField] private List<string> monsterAnimation = new List<string>();
    public void PlayMonsterAnimation(SkeletonAnimation skeletonAnimation)
    {
        if(monsterAnimation.Count > 0) 
        {
            monsterAnimation.Clear();
        }
        SkeletonDataAsset skeletonDataAsset = skeletonAnimation.SkeletonDataAsset;
        SkeletonData skeletonData = skeletonDataAsset.GetSkeletonData(true);
        var test = skeletonData.Animations.Items;
        for (int i = 0; i < test.Length; ++i)
        {
            if(test[i].Name.Contains("hit"))
            {
                monsterAnimation.Add(test[i].Name);
            }
        }
    }
}
