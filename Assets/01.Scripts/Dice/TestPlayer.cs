using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
        if (this.ChangeMyDice(DiceManager.Instance.mapCenter))
        {
            transform.position = myDice.transform.position;
        }
        var aaa = DiceManager.Instance.GetDicesWithPattern(myPos, "111\n000\n000");
        foreach (var a in aaa)
        {
            Vector2Int rotated = DiceManager.Instance.GetRotatedDiceKey(a.diceKey, myPos, EDirection.LeftUp);
            Debug.Log(a.diceKey - myPos);
            Debug.Log(rotated);
            DiceManager.Instance.TryGetDice(myPos + rotated, out var rotatedDice);
            if (rotatedDice != null)
                Debug.DrawLine(transform.position, rotatedDice.transform.position, Color.red, 60f);
            Debug.DrawLine(transform.position, a.transform.position, Color.blue, 60f);
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
