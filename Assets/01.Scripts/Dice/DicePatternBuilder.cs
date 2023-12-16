using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DicePatternBuilder
{
    public delegate IEnumerable<Dice> PatternDelegate();

    Dictionary<string, Dictionary<int, PatternDelegate>> _dicePatterns = new();
    Dictionary<string, int> _subPatternNums = new();

    private string _currentPatternName = string.Empty;
    private int _currentSubPatternNum = 0;

    public void AddPattern(string patternName, PatternDelegate subPatternAction)
    {
        _subPatternNums.TryAdd(patternName, 0);
        _dicePatterns.TryAdd(patternName, new());
        _dicePatterns[patternName].Add(_subPatternNums[patternName], subPatternAction);
        _subPatternNums[patternName] = _subPatternNums[patternName] + 1;
    }

    public void ChangePattern(string patternName, int subPatternNum = -1)
    {
        _currentPatternName = patternName;
        _currentSubPatternNum = subPatternNum;
    }

    public void SetSubPatternNum(int subPatternNum)
    {
        _currentSubPatternNum = subPatternNum;
    }

    public IEnumerable<Dice> Get(int subPatternNum = -1)
    {
        if (subPatternNum == -1)
        {
            subPatternNum = _currentSubPatternNum;
        }
        if (!_dicePatterns[_currentPatternName].ContainsKey(subPatternNum))
        {
            Debug.LogWarning($"PatternNum 존재하지 않음 PatternName : {_currentPatternName} / PatternNum : {subPatternNum}");
            return null;
        }
        return _dicePatterns[_currentPatternName][subPatternNum].Invoke();
    }

    public IEnumerable<Dice> Next()
    {
        if(!_dicePatterns[_currentPatternName].ContainsKey(_currentSubPatternNum + 1))
        {
            Debug.LogWarning($"PatternNum 초과 PatternName : {_currentPatternName} / PatternNum : {_currentSubPatternNum}");
            return null;
        }
        _currentSubPatternNum++;
        return _dicePatterns[_currentPatternName][_currentSubPatternNum].Invoke();
    }
}
