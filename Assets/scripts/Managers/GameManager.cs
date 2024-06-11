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
    public float TailAttackTimeScale;
    public float kickTimeScale;
    public float fireAttackTimeScale;


    public PlayerSystem player;
    public Transform bossWaitPosition;

    public Transform lowerAttackPoint;
    void Awake()
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

    public void AnimationController(string animationName)
    {
        TrackEntry kickTrackEntry = skeleton.AnimationState.SetAnimation(0, animationName, true);

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
        else if (animationName == "tail attack")
        {
            kickTrackEntry.TimeScale = kickTimeScale;
        }
        else if (animationName == "fire attack")
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
    public void PlayMonsterAnimation(SkeletonAnimation skeletonAnimation, string animationString = "Hit")
    {
        if (monsterAnimation.Count > 0)
        {
            monsterAnimation.Clear();
        }
        SkeletonDataAsset skeletonDataAsset = skeletonAnimation.SkeletonDataAsset;
        SkeletonData skeletonData = skeletonDataAsset.GetSkeletonData(true);
        var test = skeletonData.Animations.Items;
        float delay = 0f;
        for (int i = 0; i < test.Length; ++i)
        {
            if (test[i].Name.Contains(animationString))
            {
                monsterAnimation.Add(test[i].Name);
                delay = test[i].Duration; // 애니메이션의 지속 시간을 딜레이로 설정
            }
        }
        skeletonAnimation.AnimationState.SetAnimation(0, monsterAnimation[0], false);
        //skeletonAnimation.AnimationState.AddAnimation(0, "idle", true, delay); // 딜레이 값을 AddAnimation에 적용
    }

    public void PlayAnimation(SkeletonAnimation skeletonAnimation, string animationString, bool loop = false)
    {
        skeletonAnimation.AnimationState.ClearTracks(); // 모든 애니메이션 트랙을 제거
        SkeletonDataAsset skeletonDataAsset = skeletonAnimation.SkeletonDataAsset;
        SkeletonData skeletonData = skeletonDataAsset.GetSkeletonData(true);
        var animations = skeletonData.Animations.Items;
        float delay = 0f;
        // animationString에 해당하는 애니메이션의 Duration을 찾아 delay로 설정
        foreach (var anim in animations)
        {
            if (anim.Name == animationString)
            {
                delay = anim.Duration;
                break;
            }
        }

        skeletonAnimation.skeleton.SetToSetupPose();
        if (animationString == "fly")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animationString, loop).TimeScale = flyTimeScale;

        }
        else if (animationString == "tail attack")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animationString, loop).TimeScale = TailAttackTimeScale;

        }
        else if (animationString == "fire attack")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animationString, loop).TimeScale = fireAttackTimeScale;

        }
        else if (animationString == "tail attack2")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animationString, loop).TimeScale = 15f;
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animationString, loop).TimeScale = kickTimeScale;


        }


        if (loop == false)
        {
            skeletonAnimation.AnimationState.AddAnimation(0, "Running", true, delay); // 찾은 딜레이 값을 AddAnimation에 적용
        }
    }
}