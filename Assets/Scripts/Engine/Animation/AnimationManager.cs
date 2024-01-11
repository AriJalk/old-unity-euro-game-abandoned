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
        private int animationsRunning = 0;

        public bool IsAnimationsRunning
        {
            get { return animationsRunning > 0; }
        }

        public void StartAnimation(IAnimationContainer animatedObject, string animationName)
        {
            // Hash trigger string if needed
            if (!stringHashDictionary.ContainsKey(animationName))
            {
                stringHashDictionary.Add(animationName, Animator.StringToHash(animationName));
            }
            animatedObject.Animator.Play(stringHashDictionary[animationName]);
            animationsRunning++;
        }

        public void OnAnimationEnd(AnimatedObject animatedObject)
        {
            animationsRunning--;
            if (animationsRunning == 0)
            {
                Debug.Log("All animation ended");
            }
        }


    }
}
