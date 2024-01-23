using EDBG.Director;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CodeAnimationBase : MonoBehaviour
{
    public GameDirector GameDirector;
    public abstract void PlayAnimation();
}
