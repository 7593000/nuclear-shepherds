using System.Collections.Generic;
using UnityEngine;
//TODO=> переименовать в GameDB 
public class GameSettings : MonoBehaviour
{
    private GameData _gameData;
    private SaveLoadEngine _saveLoadEngine;
    [SerializeField] List<UnitConfig> _configEnemyUnit = new();
    [SerializeField] List<UnitConfig> _configFriendUnit = new();
    [SerializeField] private Sprite[] _spriteLevel;
    [SerializeField, Tooltip("Стартовое количество монет")] private int _startCoins;

 


    /// <summary>
    /// Получить список конфигов дружеский юнитов
    /// </summary>
    public IReadOnlyList<UnitConfig> GetFriendsConfigs => _configFriendUnit;

    /// <summary>
    /// Получить список конфигов вражеских
    /// </summary>
    public IReadOnlyList<UnitConfig> GetEnemiesConfigs => _configEnemyUnit;

    /// <summary>
    /// Взять максимальный возможный уровень 
    /// </summary>
    public int GetMaxLevel => _spriteLevel.Length;
    /// <summary>
    /// Взять спрайт уровня
    /// </summary>

    public int GetStartCoins => _startCoins;
   
    
    public void Initialized()
    {
        _gameData = new GameData();
        _saveLoadEngine = new SaveLoadEngine( _gameData );
    }

    public GameData GetGameData => _gameData;
    public void AddUnit(UnitComponent unit)
    {
        if(_gameData == null)
        {
            Initialized();
        }


        int unitId = unit.GetConfig.GetId;
        Vector3Int cellPosition = unit.CellPosition;
        int unitLevel = unit.GetUnitData.Level;

        Debug.Log( unitId );
        if ( !_gameData.UnitsData.ContainsKey( unitId ) )
        {
            _gameData.UnitsData[ unitId ] = new Dictionary<Vector3Int , int>();
        }


        _gameData.UnitsData[ unitId ][ cellPosition ] = unitLevel;
    }

    public void RemoteUnit(UnitComponent unit)
    {
        int unitId = unit.GetConfig.GetId;
        if ( !_gameData.UnitsData.ContainsKey( unitId ) )
        {
            _gameData.UnitsData.Remove(unitId );
        }
    }

    public void SaveGame()
    {
        _gameData.Wave = 5;
        _gameData.Coins = 1000;

       _saveLoadEngine.SaveData();
    }
    public Sprite GetSpriteLevel(int index)
    {
        if(index-1 <= _spriteLevel.Length)
        return _spriteLevel[index-1];

        return null;
    }  
}
