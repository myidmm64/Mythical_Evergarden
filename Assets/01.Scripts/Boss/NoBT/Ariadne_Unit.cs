using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class WeightedRandomableObject<T> 
{
    public string name;
    public T obj;
    public float weight;
    [HideInInspector]
    public float maxWeight;
}

public class WeightedRandomController<T> 
{
    private float _maxWeight = 0f;
    private List<WeightedRandomableObject<T>> _items = new List<WeightedRandomableObject<T>>();

    public WeightedRandomController(List<WeightedRandomableObject<T>> items)
    {
        _items = items;
        foreach (var item in _items)
        {
            _maxWeight += item.weight;
            item.maxWeight = _maxWeight;
        }
    }

    public T GetWeightedRandomObj()
    {
        // 가중치 랜덤
        float random = Random.Range(0f, _maxWeight);
        foreach (var item in _items)
        {
            if (random <= item.maxWeight)
                return item.obj;
        }
        return default(T);
    }
}

public class Ariadne_Unit : BossUnit
{
    [SerializeField]
    private List<WeightedRandomableObject<BossPattern>> _patterns = new List<WeightedRandomableObject<BossPattern>>();
    private WeightedRandomController<BossPattern> _randomControl = null;

    private DicePatternLinker _patternLinker = null;
    private BossPattern _currentPattern = null;
    private Coroutine _patternCoroutine = null;

    [Header("Move Params")]
    public float _upTime = 0.5f;
    public float _downTime = 0.5f;
    public float _moveTime = 1f;

    [Space(20)]
    [SerializeField]
    private PlayerBase _player = null;
    private Sequence _moveSeq = null;

    protected override void Start()
    {
        base.Start();
        Init();
        _patternCoroutine = StartCoroutine(PatternCoroutine());
    }

    private void Init()
    {
        if(_player == null)
        {
            _player = GameObject.FindObjectOfType<PlayerBase>();
        }
        _patternLinker = new DicePatternLinker();
        _randomControl = new WeightedRandomController<BossPattern>(_patterns);
        foreach (var pattern in _patterns)
        {
            pattern.obj.InitPattern(this, _patternLinker);
        }
    }

    private IEnumerator PatternCoroutine()
    {
        while (true)
        {
            if(_currentPattern == null)
                _currentPattern = _randomControl.GetWeightedRandomObj();

            Debug.Log($"패턴 실행 : {_currentPattern.GetType().ToString()}");
            _currentPattern.patternState = PatternState.Running;
            _currentPattern.PatternStart();
            yield return new WaitUntil(()=> _currentPattern.patternState == PatternState.Success);
            _currentPattern.patternState = PatternState.None;

            _currentPattern = _currentPattern.GetSubPattern();
        }
    }

    public Vector2Int GetPlayerPos => _player.myPos;

    public Sequence Move(Vector2Int targetPos)
    {
        if(_moveSeq != null)
        {
            _moveSeq.Kill();
        }
        _moveSeq = DOTween.Sequence();
        _moveSeq.Append(transform.DOMoveY(transform.position.y + 0.5f, _upTime));
        _moveSeq.Append(transform.DOMove(new Vector3(targetPos.x, targetPos.y + 0.5f), _moveTime));
        _moveSeq.Append(transform.DOMoveY(transform.position.y - 0.5f, _downTime));
        return _moveSeq;
    }

    public Sequence MoveAndAttack(Vector2Int targetPos, IEnumerable<Dice> attackRange)
    {
        return Move(targetPos).InsertCallback(_upTime + _moveTime, ()=> Attack(attackRange));
    }

    private void Attack(IEnumerable<Dice> attackRange)
    {
        foreach (var dice in attackRange)
        {
            dice.RollDiceWithRandom(1, 7);
            dice.ColorAnimation(_downTime);
        }
    }
}
