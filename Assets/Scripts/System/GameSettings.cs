using System.Collections.Generic;
using UnityEngine;

// TODO: Переименовать в GameDB
public class GameSettings : MonoBehaviour
{
    private GameHub _gameHub;
    private GameData _gameData;
    private SaveLoadEngine _saveLoadEngine;
   [SerializeField] private AudioClip _musicBackground;//пусть будет тут 

    [SerializeField] private List<UnitConfig> _configEnemyUnit = new();
    [SerializeField] private List<UnitConfig> _configFriendUnit = new();
    [SerializeField, Tooltip("Спрайты уровеней")] private Sprite[] _spriteLevel;
    [SerializeField, Tooltip("Стартовое значение волны")] private int _wave = 1;
    [SerializeField, Tooltip("Стартовое количество монет")] private int _startCoins = 140;

    private List<UnitComponent> _friendsActive = new();
    public IReadOnlyList<UnitConfig> GetFriendsConfigs => _configFriendUnit;
    public IReadOnlyList<UnitConfig> GetEnemiesConfigs => _configEnemyUnit;
    public IReadOnlyList<UnitComponent> GetFriendsActive=>_friendsActive;
    /// <summary>
    /// Получить максимальный уровень юнита
    /// </summary>
    public int GetMaxLevel => _spriteLevel.Length;
    /// <summary>
    /// Получить количество монет при старте игры
    /// </summary>
    public int GetStartCoins => _gameData.Coins;

    public GameData GetGameData => _gameData;
    /// <summary>
    /// Максимальное количество сохранений
    /// </summary>
    public int GetMaxSaveGame => _saveLoadEngine.GetMaxSaveData;
    /// <summary>
    /// Получить имена файлов сохранялок
    /// </summary>
    public string GetSaveGame => _saveLoadEngine.LoadData();

    public void Initialized(GameHub gameHub)
    {
        Time.timeScale = 1f;
        _gameHub = gameHub;

        if (GameState.Instance.IsLoading)
        {
            Debug.Log("Загрузка gameData из  GameState.");
            _gameData = GameState.Instance.LoadedGameData;
         
        }
        else
        {
            Debug.Log("Инициализация новых данных");
            _gameData = new GameData(_wave, _startCoins);
        }

        _saveLoadEngine = new SaveLoadEngine(_gameData);

        SoundEngine.Instance.PlaySound( _musicBackground , SoundType.Music);
        SoundEngine.Instance.SetVolume(0.6f, SoundType.Music);
    }

    /// <summary>
    /// Добавить бро юнита для дальнейшего сохранения его 
    /// </summary>
    /// <param name="unit"></param>
    public void AddUnit(UnitComponent unit)
    {
        //if (_gameData == null)
        //{
        //    Debug.LogError("_gameData не инициализирован!");
        //    return;
        //}


        //if (_gameData.UnitsData == null)
        //{
        //    Debug.LogError("_gameData.UnitsData не инициализирован!");
        //    _gameData.UnitsData = new Dictionary<int, Dictionary<Vector3Int, int>>();
        //}

        //int unitId = unit.GetConfig.GetId;
        //Vector3Int cellPosition = unit.CellPosition;
        //int unitLevel = unit.GetUnitData.Level;

      
        //if (!_gameData.UnitsData.ContainsKey(unitId))
        //{
        //    _gameData.UnitsData[unitId] = new Dictionary<Vector3Int, int>();
        //}

        //// Добавляем или обновляем данные о позиции и уровне юнита
        //_gameData.UnitsData[unitId][cellPosition] = unitLevel;
        _friendsActive.Add(unit);
    }

    public void RemoveUnit(UnitComponent unit)
    {


        //int unitId = unit.GetConfig.GetId;
        //if (_gameData.UnitsData.ContainsKey(unitId))
        //{
        //    _gameData.UnitsData.Remove(unitId);
        //}
        if(_friendsActive.Contains(unit)) _friendsActive.Remove(unit);
    }

    public void UpdateLevelUnit( )
    {
        if (_gameData == null)
        {
            Debug.LogError("_gameData не инициализирован!");
            return;
        }
        if (_gameData.UnitsData == null)
        {
            Debug.LogError("_gameData.UnitsData не инициализирован!");
            _gameData.UnitsData = new Dictionary<int, Dictionary<Vector3Int, int>>();
        }


        foreach (var unit in _friendsActive)
        { 
            int unitId = unit.GetConfig.GetId;
            Vector3Int cellPosition = unit.CellPosition;
            int unitLevel = unit.GetUnitData.Level;
           

            if (!_gameData.UnitsData.ContainsKey(unitId))
            {
                _gameData.UnitsData[unitId] = new Dictionary<Vector3Int, int>();
            }

            // Добавляем или обновляем данные о позиции и уровне юнита
            _gameData.UnitsData[unitId][cellPosition] = unitLevel;
        }

        
      

       
    }

    public void SaveGame()
    {
        
        _gameData.Wave = _gameHub.GetWaveEngine.GetWaveNumber;
        _gameData.Coins = _gameHub.GetWalletEngine.GetWallet.Coins;
        UpdateLevelUnit();

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
