using EDBG.Director;
using EDBG.Engine.Animation;
using EDBG.Engine.Core;
using EDBG.GameLogic.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour, IAnimationContainer
{
    private GameDirector director;

    public Transform Model;
    public Animator Animator { get; protected set; }
    public int AnimationHash { get; set; }
    public bool IsLooping { get; set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        director = GameObject.Find("Director").GetComponent<GameDirector>();
    }

    public void OnAnimationEnd()
    {
        director.OnAnimationEnd(this);
    }

}
