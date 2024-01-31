using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class MoveToPlayer : Action
{
    [SerializeField]
    private SharedGameObject _playerObj;
    [SerializeField]
    private float _animationDuration = 1f;

    private Dice _targetDice = null;
    private bool _isRunning = false;
    private Sequence _animationSeq = null;

    public override void OnStart()
    {
        base.OnStart();
        _isRunning = true;
        _targetDice = _playerObj.Value.GetComponent<IDiceUnit>().myDice;
        int randomX = Random.Range(1, DiceManager.Instance.mapSize.x + 1);
        int randomY = Random.Range(1, DiceManager.Instance.mapSize.y + 1);
        if (DiceManager.Instance != null)
        {
            if(DiceManager.Instance.TryGetDice(new Vector2Int(randomX, randomY), out Dice dice))
            {
                _targetDice = dice;
            }
        }
        if (_targetDice == null)
        {
            Debug.Log("player의 targetDice가 null 이었습니다.");
            return;
        }    
        if (_animationSeq != null)
        {
            _animationSeq.Kill();
        }
        _animationSeq = DOTween.Sequence();
        _animationSeq.Append(gameObject.transform.DOMove(_targetDice.transform.position, _animationDuration));
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();
        if(_animationSeq == null)
        {
            return TaskStatus.Success;
        }
        return _animationSeq.active ? TaskStatus.Running : TaskStatus.Success;
    }
}
