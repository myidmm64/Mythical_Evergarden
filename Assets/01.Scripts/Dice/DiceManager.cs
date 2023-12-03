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

    public Vector2Int mapSize { get; private set; }

    [SerializeField]
    private DiceGenerateDataSO _diceGenerateDataSO = null;

    private void Awake()
    {
        _diceGenerator = new DiceGenerator();
        GenerateMap();
        _diceSelector = new DiceSelector(_dices, mapSize);
    }

    private void GenerateMap()
    {
        _diceGenerator.GenerateDices(_dices, _diceGenerateDataSO, transform, out int maxRow, out int maxColumn);
        mapSize = new Vector2Int(maxRow, maxColumn);
    }

    public Dice GetDice(Vector2Int position) => _diceSelector.GetDice(position);
    public IEnumerable<Dice> GetSamePipDices(int dicePip) => _diceSelector.GetSamePipDices(dicePip);
    public IEnumerable<Dice> GetDiceRow(int rowNum) => _diceSelector.GetDiceRow(rowNum);
    public IEnumerable<Dice> GetDiceColumn(int columnNum) => _diceSelector.GetDiceColumn(columnNum);
    public IEnumerable<Dice> GetDiceLine(Vector2Int startPos, EDirection direction) => _diceSelector.GetDiceLine(startPos, direction);
    public IEnumerable<Dice> GetDiceLine(Vector2Int startPos, EDirection direction, int count) => _diceSelector.GetDiceLine(startPos, direction, count);
}
