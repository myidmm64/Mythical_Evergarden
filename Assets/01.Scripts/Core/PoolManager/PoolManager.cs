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
            Debug.LogWarning("rootTrm ����.");
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
    /// targetObj�� Pool ��ųʸ� �ȿ� �����մϴ�.
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
    /// Pool ��ųʸ����� type�� �´� PoolableObject�� ��ȯ�մϴ�. ���� ���ٸ� ���� �����Ͽ� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public PoolableObject Pop(EPoolType type)
    {
        if (!_poolDic.ContainsKey(type))
        {
            Debug.LogError("Ÿ�Կ� �´� Ű ����.");
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
    /// type�� �°� Pop�� �� T�� ����ȯ�մϴ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public T Pop<T>(EPoolType type) where T : PoolableObject
    {
        return Pop(type) as T;
    }

    /// <summary>
    /// type�� �°� Pop�� �� parent�� �ڽ����� ����ϴ�.
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