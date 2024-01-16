using EDBG.Engine.Core;
using EDBG.GameLogic.Actions;
using EDBG.States;
using System.Collections.Generic;
using UnityEngine;

namespace EDBG.Director
{
    public sealed class GameDirector : MonoBehaviour
    {
        public MapHolder MapHolder;

        Dictionary<string, int> stringHashDictionary = new Dictionary<string, int>();

        Queue<ActionBase> actionsSequence = new Queue<ActionBase>();

        Queue<ActionBase> simultaniousActions = new Queue<ActionBase>();

        List<AnimatedObject> loopingAnimations = new List<AnimatedObject>();

        Queue<AnimatedObject> animationsToStart = new Queue<AnimatedObject>();

        List<AnimatedObject> simultaniousAnimations = new List<AnimatedObject>();

        Queue<AnimatedObject> animationsToStop = new Queue<AnimatedObject>();

        Queue<AnimatedObject> animationsInSequence = new Queue<AnimatedObject>();

        bool isSequenceAnimationStopped = true;

        public bool IsGameLocked
        {
            get
            {
                return animationsInSequence.Count > 0 || animationsToStart.Count > 0 || animationsToStop.Count > 0;
            }
        }


        private void Awake()
        {
            stringHashDictionary.Add("Empty", Animator.StringToHash("Empty"));
        }

        private void Start()
        {
            MapHolder.Initialize(4, 4);
        }

        private void Update()
        {
            while (simultaniousActions.Count > 0)
            {
                ActionBase action = simultaniousActions.Dequeue();
                action.ExecuteAction();
            }
            if (actionsSequence.Count > 0)
            {
                ActionBase action = actionsSequence.Dequeue();
                action.ExecuteAction();
            }
        }

        private void LateUpdate()
        {
            while (animationsToStop.Count > 0)
            {
                AnimatedObject animatedObject = animationsToStop.Dequeue();
                // Internal hash field is not replaced in case a stopped object will also start in the update
                animatedObject.Animator.Play(stringHashDictionary["Empty"]);
            }
            while (animationsToStart.Count > 0)
            {
                AnimatedObject animatedObject = animationsToStart.Dequeue();
                animatedObject.PlayAnimation();
                simultaniousAnimations.Add(animatedObject);
            }
            if (animationsInSequence.Count > 0 && isSequenceAnimationStopped == true)
            {
                isSequenceAnimationStopped = false;
                animationsInSequence.Peek().PlayAnimation();
            }
            foreach (AnimatedObject animatedObject in loopingAnimations)
            {
                //TODO: better timing
                animatedObject.PlayAnimation();
            }

        }

        public void Initialize()
        {

        }

        public void StopAllAnimations()
        {

            while(loopingAnimations.Count > 0)
            {
                animationsToStop.Enqueue(loopingAnimations[0]);
                loopingAnimations.RemoveAt(0);
            }

            while(simultaniousAnimations.Count > 0)
            {
                animationsToStop.Enqueue(simultaniousAnimations[0]);
                simultaniousAnimations.RemoveAt(0);
            }
            while(animationsToStart.Count > 0)
            {
                animationsToStop.Enqueue(animationsToStart.Dequeue());
            }
            while(loopingAnimations.Count > 0)
            {
                animationsToStop.Enqueue(loopingAnimations[0]);
                loopingAnimations.RemoveAt(0);
            }
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

        public void AddAnimationSimultanious(AnimatedObject animatedObject, string animationName)
        {
            // Hash trigger string if needed
            if (!stringHashDictionary.ContainsKey(animationName))
            {
                stringHashDictionary.Add(animationName, Animator.StringToHash(animationName));
            }
            animatedObject.AnimationHash = stringHashDictionary[animationName];
            animationsToStart.Enqueue(animatedObject);
        }
        public void AddAnimationLooping(AnimatedObject animatedObject, string animationName)
        {
            // Hash trigger string if needed
            if (!stringHashDictionary.ContainsKey(animationName))
            {
                stringHashDictionary.Add(animationName, Animator.StringToHash(animationName));
            }
            animatedObject.AnimationHash = stringHashDictionary[animationName];
            loopingAnimations.Add(animatedObject);
        }

        public void OnAnimationEnd(AnimatedObject animatedObject)
        {
            if (animatedObject.IsLooping == false)
            {
                if (animationsInSequence.Contains(animatedObject))
                {
                    animationsToStop.Enqueue(animatedObject);
                    animationsInSequence.Dequeue();
                    isSequenceAnimationStopped = true;
                }
                else
                {
                    animationsToStop.Enqueue(animatedObject);
                    simultaniousAnimations.Remove(animatedObject);

                }
            }
        }

        public void BuildGameState(LogicState state, bool isAnimated)
        {
            GameEngineManager.Instance.MapRenderer.RenderMap(state.MapGrid, MapHolder, isAnimated);
        }

    }
}