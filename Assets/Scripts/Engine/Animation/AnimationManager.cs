using System;
using System.Collections.Generic;
using UnityEngine;

namespace EDBG.Engine.Animation
{
    public class AnimationManager : MonoBehaviour
    {
        public delegate void AnimationCompletedEventHandler();

        public event AnimationCompletedEventHandler AnimationCompleted;

        private Dictionary<string, int> stringHashDictionary = new Dictionary<string, int>();
        private List<AnimatedObject> runningAnimators = new List<AnimatedObject>();

        public bool IsAnimationsRunning
        {
            get { return runningAnimators.Count > 0; }
        }

        private void Awake()
        {
            stringHashDictionary.Add("Empty", Animator.StringToHash("Empty"));
        }

        public void StartAnimation(AnimatedObject animatedObject, string animationName)
        {
            // Hash trigger string if needed
            if (!stringHashDictionary.ContainsKey(animationName))
            {
                stringHashDictionary.Add(animationName, Animator.StringToHash(animationName));
            }
            animatedObject.Animator.Play(stringHashDictionary[animationName]);
            runningAnimators.Add(animatedObject);
            Debug.Log("Animation: " + animationName);
        }

        public void StopAllAnimations()
        {
            while (runningAnimators.Count > 0)
            {
                AnimatedObject animatedObject = runningAnimators[0];
                animatedObject.IsLooping = false;
                animatedObject.Animator.Play(stringHashDictionary["Empty"]);
                runningAnimators.RemoveAt(0);
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
