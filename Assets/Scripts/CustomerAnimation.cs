using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class CustomerAnimation : MonoBehaviour
{
    [SpineAnimation]
    public string idleAnimationName;

    [SpineAnimation]
    public string angryAnimationName;

    [SpineAnimation]
    public string happyAnimationName;

    SkeletonAnimation skeletonAnimation;

    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    void Start () {
        // Make sure you get these AnimationState and Skeleton references in Start or Later.
        // Getting and using them in Awake is not guaranteed by default execution order.
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
        
        spineAnimationState.SetAnimation(0, idleAnimationName, true);
        //spineAnimationState.AddAnimation(0, happyAnimationName, false, 0);
    }

    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            timer = 0f;
            spineAnimationState.SetAnimation(0, angryAnimationName, false);
            spineAnimationState.AddAnimation(0, happyAnimationName, false, 0);
            spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
        }
    }
}
