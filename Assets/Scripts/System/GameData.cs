
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public sealed class GameData
{
    public int _wave;
    public int _coins;
    public int _brahmin;
    public Dictionary<int, Dictionary<Vector3Int, int>> _unitsData = new();

    public GameData(int brahmin,int wave, int coins, Dictionary<int, Dictionary<Vector3Int, int>> positionAndLevel = null)
    {
        _brahmin = brahmin;
        _wave = wave;
        _coins = coins;

        if (positionAndLevel != null)
        {
            _unitsData = positionAndLevel;
        }

    }
    public int Brahmin { get => _brahmin; set => _brahmin = value; }
    public int Wave { get => _wave; set => _wave = value; }
    public int Coins { get => _coins; set => _coins = value; }

    /// <summary>
    /// Получить словарь: <unitID , Vector3IntPosition, Level>
    /// </summary>
    public Dictionary<int, Dictionary<Vector3Int, int>> UnitsData
    {
        get => _unitsData;
        set => _unitsData = value;
    }
}
