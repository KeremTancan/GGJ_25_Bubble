using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
using System;

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

    private Action OnHappyAnimationComplete;
    private Action OnAngryAnimationComplete;
    

    void Start () {
        // Make sure you get these AnimationState and Skeleton references in Start or Later.
        // Getting and using them in Awake is not guaranteed by default execution order.
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
        skeletonAnimation.AnimationState.Complete += OnAnimationComplete;

        PlayIdleAnimation();
        //spineAnimationState.AddAnimation(0, happyAnimationName, false, 0);
    }
    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        //Debug.Log($"Animation {trackEntry.Animation.Name} completed!");

        // Ensure it only triggers for the desired animation
        if (trackEntry.Animation.Name == happyAnimationName)
        {
            OnHappyAnimationComplete?.Invoke();
        }
        if (trackEntry.Animation.Name == angryAnimationName)
        {
            OnAngryAnimationComplete?.Invoke();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (skeletonAnimation != null)
        {
            skeletonAnimation.AnimationState.Complete -= OnAnimationComplete;
        }
    }
    
    public void AddIdleAnimation()
    {
        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
    }
    public void PlayIdleAnimation(bool loop = true)
    {
        spineAnimationState.SetAnimation(0, idleAnimationName, loop);
    }
    
    public void PlayAngryAnimation(Action onComplete,bool loop = false)
    {
        OnAngryAnimationComplete += onComplete;
        spineAnimationState.SetAnimation(0, angryAnimationName, loop);
    }
    
    public void PlayHappyAnimation(Action onComplete,bool loop = false)
    {
        OnHappyAnimationComplete += onComplete;
        spineAnimationState.SetAnimation(0, happyAnimationName, loop);
    }
    
    

    /*private float timer;
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
    }*/
}
