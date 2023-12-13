using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
        var normalPattern = DiceManager.Instance.GetDicesWithPattern(myPos, "111\n000\n000");
        var rotatedPattern = DiceManager.Instance.GetDicesWithPattern(myPos, "111\n000\n000", EDirection.Left);
        foreach(var normal in normalPattern)
        {
            Debug.Log("normal : " + normal.diceKey);
            Debug.DrawLine(transform.position, normal.transform.position, Color.red, 60f);
        }
        foreach (var rotated in rotatedPattern)
        {
            Debug.Log("rotated : " + rotated.diceKey);
            Debug.DrawLine(transform.position, rotated.transform.position, Color.blue, 60f);
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
            if(this.ChangeMyDice(myPos + Vector2Int.FloorToInt(moveAmount)))
            {
                transform.position = myDice.transform.position;
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
        exitedDice.RollDiceWithRandom(1, 7);
        exitedDice.ColorAnimation();
    }
}
