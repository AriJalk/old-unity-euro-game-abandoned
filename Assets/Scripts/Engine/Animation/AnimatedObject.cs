using EDBG.Engine.Animation;
using EDBG.Engine.Core;
using EDBG.GameLogic.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour, IAnimationContainer
{
    public Animator Animator { get; protected set; }
    public bool IsLooping { get; set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        IsLooping = false;
    }

    public void OnAnimationEnd()
    {
        GameEngineManager.Instance.AnimationManager.OnAnimationEnd(this);
    }

}
