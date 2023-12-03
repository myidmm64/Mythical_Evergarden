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

    private Dictionary<Vector2Int, Dice> _dices = new Dictionary<Vector2Int, Dice>();
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

    public Dice GetDice(Vector2Int position) => _diceSelector.GetDice(position);
    public IEnumerable<Dice> GetSamePipDices(int dicePip) => _diceSelector.GetSamePipDices(dicePip);
    public IEnumerable<Dice> GetDiceRow(int rowNum) => _diceSelector.GetDiceRow(rowNum);
    public IEnumerable<Dice> GetDiceColumn(int columnNum) => _diceSelector.GetDiceColumn(columnNum);
    public IEnumerable<Dice> GetDices(Vector2 center, int[,] pattern) => _diceSelector.GetDices(center, pattern);
}
