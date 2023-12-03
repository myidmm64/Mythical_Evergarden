using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour, IDiceUnit
{
    [SerializeField]
    private int _moveAmount = 1;
    [SerializeField]
    private Dice _dice = null;

    [SerializeField]
    private float _moveDelay = 0.5f;
    private bool _moveable = true;

    private Vector2Int _currentPos = Vector2Int.zero;

    private void Start()
    {
        transform.position = DiceManager.Instance.GetDice(_currentPos).transform.position;
        _currentPos = new Vector2Int(1, 1);
    }

    private void Update()
    {
        if (!_moveable)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 moveAmount = new Vector2(horizontal, vertical);

        if (moveAmount.sqrMagnitude > 0)
        {
            transform.position = transform.position + (Vector3)(moveAmount * _moveAmount);
            _currentPos += Vector2Int.FloorToInt(moveAmount);

            _dice = DiceManager.Instance.GetDice(_currentPos);

            StartCoroutine(MoveDelayCoroutine());
        }
    }

    private IEnumerator MoveDelayCoroutine()
    {
        _moveable = false;
        yield return new WaitForSeconds(_moveDelay);
        _moveable = true;
    }

    public void EnterDice()
    {
    }

    public void ExitDice()
    {
    }
}
