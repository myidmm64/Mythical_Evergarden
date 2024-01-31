using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ariadne_Unit : BossUnit
{
    [Header("Ariadne Custom Start")]
    [SerializeField]
    private PopupDataSO _testHitPopupData = null;
    [SerializeField]
    private int _mapHp = 4000;
    [SerializeField]
    private HPBar _hpBar = null;

    private int _curHp = 0;

    [Header("Patterns")]
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
    public float _testWaitTime = 1f;

    [Header("Other")]
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
        _curHp = _mapHp;
        _hpBar?.HpInit(_mapHp);
        if (_player == null)
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
            if (_currentPattern == null)
                _currentPattern = _randomControl.GetWeightedRandomObj();

            Debug.Log($"패턴 실행 : {_currentPattern.GetType().ToString()}");
            _currentPattern.patternState = PatternState.Running;
            _currentPattern.PatternStart();
            yield return new WaitUntil(() => _currentPattern.patternState == PatternState.Success);
            _currentPattern.patternState = PatternState.None;

            _currentPattern = _currentPattern.GetSubPattern();
        }
    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);

        _curHp -= damage;
        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.Popup(_testHitPopupData, damage.ToString(), transform.position + Vector3.up * 0.5f, null);
        }
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.CameraShake(1f, 2f, 0.1f);
        }
        _hpBar?.HpUpdate(_curHp);
    }

    public Vector2Int GetPlayerPos => _player.myPos;

    public Sequence Move(Vector2Int targetPos)
    {
        // TODO : Dice를 무조건 가져올 수 있도록, ChangeMyDice에 dice를 넣을 수 있도록
        // 해당 사항은 플레이어 밀려내기가 어떤 식으로 구현되느냐에 따라 다름.
        
        DiceManager.Instance.TryGetDice(targetPos, out var targetDice);

        if (_moveSeq != null)
        {
            _moveSeq.Kill();
        }
        Vector3 endPosition = targetDice.transform.position;
        this.ClearMyDice();
        _moveSeq = DOTween.Sequence();
        _moveSeq.Append(transform.DOMoveY(transform.position.y + 0.5f, _upTime)).SetEase(Ease.Linear);
        _moveSeq.Append(transform.DOMove(new Vector3(endPosition.x, endPosition.y + 0.5f), _moveTime)).SetEase(Ease.Linear);
        _moveSeq.Append(transform.DOMoveY(endPosition.y, _downTime)).SetEase(Ease.Linear);
        _moveSeq.AppendCallback(() => this.ChangeMyDice(targetPos, out var targetDice)); // 무조건 가져오도록
        return _moveSeq;
    }

    public Sequence MoveAndAttack(Vector2Int targetPos, IEnumerable<Dice> attackRange)
    {
        Sequence seq = Move(targetPos);
        seq.InsertCallback(_upTime, () => Attack(attackRange));
        seq.AppendCallback(() =>
        {
            if (CameraManager.Instance != null)
            {
                CameraManager.Instance.CameraShake(5f, 3f, 0.1f);
            }
        });
        return seq;
    }

    private void Attack(IEnumerable<Dice> attackRange)
    {
        foreach (var dice in attackRange)
        {
            dice.RollDiceWithRandom(1, 7);
            dice.ColorAnimation((_moveTime + _downTime) * 0.5f);
        }
    }
}
