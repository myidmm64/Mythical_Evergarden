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

    public Dice myDice { get; set; }
    public Vector2Int myPos { get; set; }

    private void Start()
    {
        myPos = new Vector2Int(1, 1);
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            transform.position = dice.transform.position;
        }
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
            myPos += Vector2Int.FloorToInt(moveAmount);

            if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
            {
                _dice = dice;
            }

            StartCoroutine(MoveDelayCoroutine());
        }
    }

    private IEnumerator MoveDelayCoroutine()
    {
        _moveable = false;
        yield return new WaitForSeconds(_moveDelay);
        _moveable = true;
    }

    public void EnterDice(Dice enterdDice)
    {
    }

    public void ExitDice(Dice exitedDice)
    {
    }
}
