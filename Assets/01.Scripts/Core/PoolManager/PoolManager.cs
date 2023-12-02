using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleTon<PoolManager>
{
    [SerializeField]
    private PoolDataScriptableObject _poolDataSO = null;
    private Dictionary<EPoolType, Queue<PoolableObject>> _poolDic = new Dictionary<EPoolType, Queue<PoolableObject>>();
    private Transform _rootTrm = null;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_rootTrm == null)
        {
            Debug.LogWarning("rootTrm 없음.");
            _rootTrm = transform;
        }

        for (int i = 0; i < _poolDataSO.poolDatas.Count; i++)
        {
            _poolDic.Add(_poolDataSO.poolDatas[i].poolType, new Queue<PoolableObject>());

            for (int j = 0; j < _poolDataSO.poolDatas[i].spawnCount; j++)
            {
                PoolableObject poolable = Instantiate(_poolDataSO.poolDatas[i].obj, _rootTrm);
                poolable.poolType = _poolDataSO.poolDatas[i].poolType;
                poolable.StartInit();
                poolable.name = poolable.name.Replace("(Clone)", "");
                poolable.gameObject.SetActive(false);
                _poolDic[poolable.poolType].Enqueue(poolable);
            }
        }
    }

    /// <summary>
    /// targetObj를 Pool 딕셔너리 안에 보관합니다.
    /// </summary>
    /// <param name="targetObj"></param>
    public void Push(PoolableObject targetObj)
    {
        targetObj.PushInit();
        targetObj.transform.SetParent(_rootTrm);
        targetObj.gameObject.SetActive(false);
        _poolDic[targetObj.poolType].Enqueue(targetObj);
    }

    /// <summary>
    /// Pool 딕셔너리에서 type에 맞는 PoolableObject를 반환합니다. 만약 없다면 새로 생성하여 반환합니다.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public PoolableObject Pop(EPoolType type)
    {
        if (!_poolDic.ContainsKey(type))
        {
            Debug.LogError("타입에 맞는 키 없음.");
            return null;
        }

        PoolableObject targetObj = _poolDic[type].Dequeue();
        if (_poolDic[type].Count == 0)
        {
            PoolableObject poolable = Instantiate(targetObj, _rootTrm);
            poolable.poolType = targetObj.poolType;
            poolable.StartInit();
            poolable.gameObject.SetActive(false);
            poolable.name = poolable.name.Replace("(Clone)", "");
            _poolDic[poolable.poolType].Enqueue(poolable);
        }

        targetObj.gameObject.SetActive(true);
        targetObj.PopInit();
        return targetObj;
    }

    /// <summary>
    /// type에 맞게 Pop한 뒤 T로 형변환합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public T Pop<T>(EPoolType type) where T : PoolableObject
    {
        return Pop(type) as T;
    }

    /// <summary>
    /// type에 맞게 Pop한 뒤 parent의 자식으로 만듭니다.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public PoolableObject Pop(EPoolType type, Transform parent)
    {
        PoolableObject target = Pop(type);
        target.transform.SetParent(parent);
        return target;
    }
}