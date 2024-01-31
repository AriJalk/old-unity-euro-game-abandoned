using EDBG.Director;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CodeAnimationBase : MonoBehaviour
{
    protected AnimationManager gameDirector;

    public void SetAnimationManager(AnimationManager gameDirector)
    {
        this.gameDirector = gameDirector;
        gameDirector.AddAnimation(this);
    }
    public abstract void PlayAnimation();
    public virtual void StopAnimation()
    {
        if( gameDirector != null )
        {
            gameDirector.OnAnimationEnd(this);
        }
    }
}
