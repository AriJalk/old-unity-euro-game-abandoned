using System;
using System.Collections.Generic;
using UnityEngine;

namespace EDBG.Engine.Animation
{
    //TODO: sync start of animation
    public class AnimationManager : MonoBehaviour
    {
        public delegate void AnimationCompletedEventHandler();

        public event AnimationCompletedEventHandler AnimationsCompleted;

        private Dictionary<string, int> stringHashDictionary = new Dictionary<string, int>();
        private List<AnimatedObject> runningAnimators = new List<AnimatedObject>();
        private Stack<AnimatedObject> animationsToStart = new Stack<AnimatedObject>();
        private Stack<AnimatedObject> animationsToStop = new Stack<AnimatedObject>();

        public bool IsAnimationsRunning
        {
            get { return runningAnimators.Count > 0; }
        }

        private void Awake()
        {
            stringHashDictionary.Add("Empty", Animator.StringToHash("Empty"));
        }

        private void LateUpdate()
        {
            while (animationsToStop.Count > 0)
            {
                AnimatedObject anim = animationsToStop.Pop();
                anim.Animator.Play(stringHashDictionary["Empty"]);
                runningAnimators.Remove(anim);
            }
            while (animationsToStart.Count > 0)
            {
                AnimatedObject anim = animationsToStart.Pop();
                anim.gameObject.SetActive(true);
                anim.Animator.Play(anim.AnimationHash);
                runningAnimators.Add(anim);
            }

        }


        public void StartAnimation(AnimatedObject animatedObject, string animationName)
        {
            // Hash trigger string if needed
            if (!stringHashDictionary.ContainsKey(animationName))
            {
                stringHashDictionary.Add(animationName, Animator.StringToHash(animationName));
            }
            animatedObject.AnimationHash = stringHashDictionary[animationName];
            animatedObject.gameObject.SetActive(false);
            animationsToStart.Push(animatedObject);
            Debug.Log("Animation: " + animationName);
        }


        public void StopAllAnimations()
        {
            for (int i = 0; i < runningAnimators.Count; i++)
            {
                AnimatedObject animObj = runningAnimators[i];
                animObj.transform.localScale = Vector3.one;
                animationsToStop.Push(animObj);
            }
        }

        public void OnAnimationEnd(AnimatedObject animatedObject)
        {
            if (animatedObject.IsLooping == false)
            {
                animatedObject.Animator.Play(stringHashDictionary["Empty"]);
                runningAnimators.Remove(animatedObject);
                if (runningAnimators.Count == 0)
                {
                    Debug.Log("All animation ended");
                }
            }
        }


    }
}
