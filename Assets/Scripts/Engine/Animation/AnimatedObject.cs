using EDBG.Engine.Animation;
using EDBG.Engine.Core;
using EDBG.GameLogic.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour, IAnimationContainer
{
    public Animator Animator { get; private set; }

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    public void OnAnimationEnd()
    {
        GameEngineManager.Instance.AnimationManager.OnAnimationEnd(this);
    }
}
