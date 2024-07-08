using System.Collections.Generic;
using UnityEngine;

// TODO: Переименовать в GameDB
public class GameSettings : MonoBehaviour
{
    private GameHub _gameHub;
    private GameData _gameData;
    private SaveLoadEngine _saveLoadEngine;

    [SerializeField] private List<UnitConfig> _configEnemyUnit = new();
    [SerializeField] private List<UnitConfig> _configFriendUnit = new();
    [SerializeField] private Sprite[] _spriteLevel;
    [SerializeField, Tooltip("Стартовое значение волны")] private int _wave = 1;
    [SerializeField, Tooltip("Стартовое количество монет")] private int _startCoins = 140;

    public IReadOnlyList<UnitConfig> GetFriendsConfigs => _configFriendUnit;
    public IReadOnlyList<UnitConfig> GetEnemiesConfigs => _configEnemyUnit;
    public int GetMaxLevel => _spriteLevel.Length;
    public int GetStartCoins => _gameData.Coins;

    public GameData GetGameData => _gameData;
    public int GetMaxSaveGame => _saveLoadEngine.GetMaxSaveData;
    public string GetSaveGame => _saveLoadEngine.LoadData();
    public void Initialized(GameHub gameHub)
    {
        Time.timeScale = 1f;
        _gameHub = gameHub;

        if (GameState.Instance.IsLoading)
        {
            Debug.Log("Загрузка gameData из  GameState.");
            _gameData = GameState.Instance.LoadedGameData;
            GameState.Instance.IsLoading = false;
        }
        else
        {
            Debug.Log("Инициализация новых данных");
            _gameData = new GameData(_wave, _startCoins);
        }

        _saveLoadEngine = new SaveLoadEngine(_gameData);
    }

    public void AddUnit(UnitComponent unit)
    {
        int unitId = unit.GetConfig.GetId;
        Vector3Int cellPosition = unit.CellPosition;
        int unitLevel = unit.GetUnitData.Level;

        if (!_gameData.UnitsData.ContainsKey(unitId))
        {
            _gameData.UnitsData[unitId] = new Dictionary<Vector3Int, int>();
        }

        _gameData.UnitsData[unitId][cellPosition] = unitLevel;
    }

    public void RemoveUnit(UnitComponent unit)
    {
        int unitId = unit.GetConfig.GetId;
        if (_gameData.UnitsData.ContainsKey(unitId))
        {
            _gameData.UnitsData.Remove(unitId);
        }
    }

    public void SaveGame()
    {
        // Для тестирования
        _gameData.Wave = 31;
        _gameData.Coins = 10000;

        _saveLoadEngine.SaveData();
    }

    public void LoadGame(string path)
    {
        _gameData = _saveLoadEngine.LoadData(path);

        if (_gameData != null)
        {
            GameState.Instance.LoadedGameData = _gameData;
            GameState.Instance.IsLoading = true;
            _gameHub.ReloadData();
        }
        else
        {
            Debug.LogError("Беда с загрузкой данных");
        }
    }

    public Sprite GetSpriteLevel(int index)
    {
        if (index - 1 < _spriteLevel.Length)
        {
            return _spriteLevel[index - 1];
        }

        Debug.LogError($"оишбка {index} в sprite level.");
        return null;
    }
}
