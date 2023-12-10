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

    private Vector3 rotated = Vector3.zero;

    private void Start()
    {
        myPos = new Vector2Int(1, 1);
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            transform.position = dice.transform.position;
        }

        myPos = new Vector2Int(1, 2);
        rotated = Quaternion.AngleAxis(90f, Vector3.forward) * new Vector2(myPos.x, myPos.y);
        Debug.Log(rotated);
        Debug.Log(Quaternion.AngleAxis(45f, Vector3.forward) * new Vector2(myPos.x, myPos.y));
        Debug.DrawLine(transform.position, transform.position + rotated, Color.red, 60f);
        Debug.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(45f, Vector3.forward) * new Vector2(myPos.x, myPos.y), Color.blue, 60f);
        Debug.DrawLine(transform.position, transform.position + new Vector3(myPos.x, myPos.y), Color.green, 60f);
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
