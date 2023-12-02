using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    [HideInInspector]
    public EPoolType poolType = EPoolType.None;

    public abstract void StartInit();
    public abstract void PopInit();
    public abstract void PushInit();
}