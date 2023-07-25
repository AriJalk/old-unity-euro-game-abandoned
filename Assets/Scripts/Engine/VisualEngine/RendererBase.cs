using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RendererBase : MonoBehaviour
{
    protected PoolManager poolManager;
    public virtual void Initialize(PoolManager pm)
    {
        poolManager = pm;
    }
    public abstract void Render(SquareMapHolderObject mapHolder, MaterialPool materialPool);
}
