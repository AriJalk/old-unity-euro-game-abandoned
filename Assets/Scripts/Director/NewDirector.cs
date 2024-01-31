using System.Collections.Generic;
using UnityEngine;


public enum GameAnimations
{
    PlaceDisc,
    Breath,
}
public class NewDirector
{
    private static NewDirector instance;
    public static NewDirector Instance
    {
        get
        {
            if(instance == null)
                instance = new NewDirector();
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private bool _isGameLocked;

    public bool IsGameLocked
    {
        get { return _isGameLocked; }
        set { _isGameLocked = value; }
    }



    private List<CodeAnimationBase> runningAnimations = new List<CodeAnimationBase>();

    public void StopAllAnimations()
    {
        while(runningAnimations.Count > 0)
        {
            CodeAnimationBase animation = runningAnimations[0];
            Debug.Log(animation.name + " Stopped");
            runningAnimations.Remove(animation);
            animation.StopAnimation();
        }
    }

    public void AddAnimation(CodeAnimationBase animation)
    {
        runningAnimations.Add(animation);
    }

    public void OnAnimationEnd(CodeAnimationBase animation)
    {
        Debug.Log("Animation end: " + animation.GetType().Name);
        runningAnimations.Remove(animation);
    }
}