using Spine;
using Spine.Unity;
using UnityEngine;

public static class Define
{
    public static void SetAni(this SkeletonAnimation skeletonAnimation, string animationString, bool loop = false)
    {
        var currentTrackEntry = skeletonAnimation.AnimationState.GetCurrent(0);
        if(animationString ==  "Running" && currentTrackEntry.Animation.Name == "Running")
        {
            return;
        }

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
        skeletonAnimation.AnimationState.SetAnimation(0, animationString, loop);

        if (loop == false)
        {
            var ani = "Running";
            if (animationString == "fire attack")
            {
                ani = "fly";
            }
            skeletonAnimation.AnimationState.AddAnimation(0, ani, true, delay); // 찾은 딜레이 값을 AddAnimation에 적용
        }
    }
}