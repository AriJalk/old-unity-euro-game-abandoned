using UnityEngine;

namespace EDBG.Engine.Animation
{
    public interface IAnimationContainer
    {
        Animator Animator { get; }
        bool IsLooping { get; set; }
    }
}