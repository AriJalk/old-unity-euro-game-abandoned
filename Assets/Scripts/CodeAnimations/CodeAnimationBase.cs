using EDBG.Director;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CodeAnimationBase : MonoBehaviour
{
    protected AnimationManager animationManager;

    public void SetAnimationManager(AnimationManager gameDirector)
    {
        this.animationManager = gameDirector;
        gameDirector.AddAnimation(this);
    }
    public abstract void PlayAnimation();
    public virtual void StopAnimation()
    {
        if( animationManager != null )
        {
            animationManager.OnAnimationEnd(this);
        }
    }
}
