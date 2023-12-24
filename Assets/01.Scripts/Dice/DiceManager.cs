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

    public Dictionary<Vector2Int, Dice> diceMap { get; private set; }
    private DiceGenerator _diceGenerator = null;
    private DiceSelector _diceSelector = null;

    public Vector2Int mapSize { get; private set; } // 맵에 dice가 삭제될 때 변경되어야할 가능 성 있음.
    public Vector2Int mapCenter => new Vector2Int(mapSize.x / 2 + 1, mapSize.y / 2 + 1); // 맵에 dice가 삭제될 때 변경되어야할 가능 성 있음.

    private void Awake()
    {
        _diceGenerator = new DiceGenerator();
        GenerateMap();
        _diceSelector = new DiceSelector(diceMap, mapSize);
    }

    private void GenerateMap()
    {
        diceMap = new ();
        _diceGenerator.GenerateDices(diceMap, _diceGenerateDataSO, transform, out int maxRow, out int maxColumn);
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
    public IEnumerable<Dice> GetDiceLine(Vector2Int startPos, EDirection direction, int count, bool plusReflect = false, EDirection rotateDirection = EDirection.Up) => _diceSelector.GetDiceLine(startPos, direction, count, plusReflect, rotateDirection);
    public IEnumerable<Dice> GetCrossDices(Vector2Int startPos, int count) => _diceSelector.GetCrossDices(startPos, count);
    public IEnumerable<Dice> GetXCrossDices(Vector2Int startPos, int count) => _diceSelector.GetXCrossDices(startPos, count);
    public IEnumerable<Dice> GetDiceSquare(Vector2Int centerPos, int size, bool isBorder = false) => _diceSelector.GetDiceSquare(centerPos, size, isBorder);
    public IEnumerable<Dice> GetDiceRotatedSquare(Vector2Int centerPos, int centerDistance, bool isBorder = false) => _diceSelector.GetDiceRotatedSquare(centerPos, centerDistance, isBorder);
    public IEnumerable<Dice> GetDiceRectangle(Vector2Int centerPos, int width, int height, bool isBorder = false, EDirection rotateDirection = EDirection.Up) => _diceSelector.GetDiceRectangle(centerPos, width, height, isBorder, rotateDirection);
    public IEnumerable<Dice> GetDicesWithPattern(Vector2Int centerPos, string pattern, EDirection rotateDirection = EDirection.Up) => _diceSelector.GetDicesWithPattern(centerPos, pattern, rotateDirection);
}
