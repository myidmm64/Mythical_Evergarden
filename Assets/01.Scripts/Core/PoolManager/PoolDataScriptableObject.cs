using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PoolData")]
public class PoolDataScriptableObject : ScriptableObject
{
    public List<PoolData> poolDatas = new List<PoolData>();
}