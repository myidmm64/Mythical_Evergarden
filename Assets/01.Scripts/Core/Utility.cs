using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static string TextAreaToDicePosition(string text)
    {
        return null;
    }

    public static Vector2Int GetDirection(EDirection direction) => direction switch
    {
        EDirection.None => Vector2Int.zero,
        EDirection.Left => Vector2Int.left,
        EDirection.Right => Vector2Int.right,
        EDirection.Up => Vector2Int.up,
        EDirection.Down => Vector2Int.down,
        EDirection.LeftUp => Vector2Int.left + Vector2Int.up,
        EDirection.RightUp => Vector2Int.right + Vector2Int.up,
        EDirection.LeftDown => Vector2Int.left + Vector2Int.down,
        EDirection.RightDown => Vector2Int.right + Vector2Int.down,
        _ => Vector2Int.zero,
    };
}
