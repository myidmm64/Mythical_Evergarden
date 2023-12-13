using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoSingleTon<DiceManager>
{
    [SerializeField]
    private DiceGenerateDataSO _diceGenerateDataSO = null;

    private Dictionary<Vector2Int, Dice> _dices = new Dictionary<Vector2Int, Dice>();
    private DiceGenerator _diceGenerator = null;
    private DiceSelector _diceSelector = null;

    public Vector2Int mapSize { get; private set; }
    public Vector2Int mapCenter => new Vector2Int(mapSize.x / 2 + 1, mapSize.y / 2 + 1);

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

    public void PlayDiceMatchingSound()
    {
        AudioManager.Instance.Play(EAudioType.DiceMatching);
    }

    public bool TryGetDice(Vector2Int position, out Dice dice) => _diceSelector.TryGetDice(position, out dice);
    public IEnumerable<Dice> GetSamePipDices(int dicePip) => _diceSelector.GetSamePipDices(dicePip);
    public IEnumerable<Dice> GetDiceRow(int rowNum) => _diceSelector.GetDiceRow(rowNum);
    public IEnumerable<Dice> GetDiceColumn(int columnNum) => _diceSelector.GetDiceColumn(columnNum);
    public IEnumerable<Dice> GetDiceLine(Vector2Int startPos, EDirection direction, int count, bool plusReflect, EDirection rotateDirection = EDirection.Up) => _diceSelector.GetDiceLine(startPos, direction, count, plusReflect, rotateDirection);
    public IEnumerable<Dice> GetCrossDices(Vector2Int startPos, int count) => _diceSelector.GetCrossDices(startPos, count);
    public IEnumerable<Dice> GetXCrossDices(Vector2Int startPos, int count) => _diceSelector.GetXCrossDices(startPos, count);
    public IEnumerable<Dice> GetDiceSquare(Vector2Int centerPos, int size) => _diceSelector.GetDiceSquare(centerPos, size);
    public IEnumerable<Dice> GetDiceRotatedSquare(Vector2Int centerPos, int size) => _diceSelector.GetDiceRotatedSquare(centerPos, size);
    public IEnumerable<Dice> GetDiceRectangle(Vector2Int centerPos, int width, int height) => _diceSelector.GetDiceRectangle(centerPos, width, height);
    public IEnumerable<Dice> GetDicesWithPattern(Vector2Int centerPos, string pattern, EDirection rotateDirection = EDirection.Up) => _diceSelector.GetDicesWithPattern(centerPos, pattern, rotateDirection);
}
