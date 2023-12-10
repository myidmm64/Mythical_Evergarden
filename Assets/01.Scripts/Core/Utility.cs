using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
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

    public static int GetMaxCountWithDirection(EDirection direction, Vector2Int mapSize) => direction switch
    {
        EDirection.None => 0,
        EDirection.Left => mapSize.x,
        EDirection.Right => mapSize.x,
        EDirection.Up => mapSize.y,
        EDirection.Down => mapSize.y,
        EDirection.LeftUp => Mathf.Min(mapSize.x, mapSize.y),
        EDirection.RightUp => Mathf.Min(mapSize.x, mapSize.y),
        EDirection.LeftDown => Mathf.Min(mapSize.x, mapSize.y),
        EDirection.RightDown => Mathf.Min(mapSize.x, mapSize.y),
        _ => 0,
    };

    public static EDirection GetReflectDirection(EDirection direction) => direction switch
    {
        EDirection.None => EDirection.None,
        EDirection.Left => EDirection.Right,
        EDirection.Right => EDirection.Left,
        EDirection.Up => EDirection.Down,
        EDirection.Down => EDirection.Up,
        EDirection.LeftUp => EDirection.RightDown,
        EDirection.RightUp => EDirection.LeftDown,
        EDirection.LeftDown => EDirection.RightUp,
        EDirection.RightDown => EDirection.LeftUp,
        _ => EDirection.None,
    };

    public static float GetZRotate(EDirection direction) => direction switch
    {
        EDirection.None => 0f,
        EDirection.Left => 90f,
        EDirection.Right => -90f,
        EDirection.Up => 0f,
        EDirection.Down => 180f,
        EDirection.LeftUp => 45f,
        EDirection.RightUp => -45f,
        EDirection.LeftDown => 135f,
        EDirection.RightDown => -135f,
        _ => 0f,
    };
}
