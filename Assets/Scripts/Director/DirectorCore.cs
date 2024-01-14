using EDBG.GameLogic.Actions;
using EDBG.GameLogic.MapSystem;
using System.Collections.Generic;
using UnityEngine;

namespace EDBG.Director
{
    public sealed class DirectorCore : MonoBehaviour
    {

        Dictionary<string, int> stringHashDictionary = new Dictionary<string, int>();
        MapHolder mapHolder;
        MapGrid mapGrid;
        Queue<ActionBase> actionsSequence = new Queue<ActionBase>();
        Queue<ActionBase> simultaniousActions = new Queue<ActionBase>();
        List<AnimatedObject> loopingAnimations = new List<AnimatedObject>();
        List<AnimatedObject> simultaniousAnimations = new List<AnimatedObject>();
        Queue<AnimatedObject> animationsToStop = new Queue<AnimatedObject>();
        Queue<AnimatedObject> animationsInSequence = new Queue<AnimatedObject>();
        AnimatedObject currentAnimation;

        public bool IsGameLocked
        {
            get
            {
                return currentAnimation == null;
            }
        }


        private void Awake()
        {
            stringHashDictionary.Add("Empty", Animator.StringToHash("Empty"));
        }

        private void Update()
        {
            while(simultaniousActions.Count > 0)
            {
                ActionBase action = simultaniousActions.Dequeue();
                action.ExecuteAction();
            }
        }

        private void LateUpdate()
        {
            while(animationsToStop.Count > 0)
            {
                AnimatedObject animatedObject = animationsToStop.Dequeue();  
                
            }
            if (currentAnimation == null)
            {
                ActionBase action = actionsSequence.Dequeue();
                action.ExecuteAction();
            }
        }

        public void Initialize()
        {

        }


        // Call only in late update after updating hash code in animatedObject
        private void PlayAnimation(AnimatedObject animatedObject)
        {
            
            animatedObject.Animator.Play(animatedObject.AnimationHash);
        }

        public void AddActionToSequence(ActionBase action)
        {
            actionsSequence.Enqueue(action);
        }

        public void AddActionSimultanious(ActionBase action)
        {
            simultaniousActions.Enqueue(action);
        }

        public void AddAnimationToSequence(AnimatedObject animatedObject, string animationName)
        {
            // Hash trigger string if needed
            if (!stringHashDictionary.ContainsKey(animationName))
            {
                stringHashDictionary.Add(animationName, Animator.StringToHash(animationName));
            }
            animatedObject.AnimationHash = stringHashDictionary[animationName];
            animationsInSequence.Enqueue(animatedObject);
        }

        public void OnAnimationEnd(AnimatedObject animatedObject)
        {
            if (animatedObject.IsLooping == false)
            {
                if(animatedObject == currentAnimation)
                {
                    animationsToStop.Enqueue(animatedObject);
                    currentAnimation = null;
                }
                else
                {
                    animationsToStop.Enqueue(animatedObject);
                    simultaniousAnimations.Remove(animatedObject);

                }
            }
        }

    }
}