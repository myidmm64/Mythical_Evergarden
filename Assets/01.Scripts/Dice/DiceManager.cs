using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoSingleTon<DiceManager>
{
    [SerializeField]
    private TestPlayer _player = null;

    private Dictionary<Vector2, Dice> _dices = new Dictionary<Vector2, Dice>();
    private DiceGenerator _diceGenerator = null;
    private DiceSelector _diceSelector = null;

    [SerializeField]
    private DiceGenerateDataSO _diceGenerateDataSO = null;

    private void Awake()
    {
        _diceGenerator = new DiceGenerator();
        _diceGenerator.GenerateDices(_dices, _diceGenerateDataSO, transform);
        _diceSelector = new DiceSelector(_dices);
    }

    public Dice GetDice(int row, int column) => _diceSelector.GetDice(row, column);
    public IEnumerable<Dice> GetSamePipDices(int dicePip) => _diceSelector.GetSamePipDices(dicePip);
    public IEnumerable<Dice> GetDiceRow(int row) => _diceSelector.GetDiceRow(row);
    public IEnumerable<Dice> GetDiceColumn(int column) => _diceSelector.GetDiceColumn(column);
    public IEnumerable<Dice> GetDices(Vector2 center, int[,] pattern) => _diceSelector.GetDices(center, pattern);
}
