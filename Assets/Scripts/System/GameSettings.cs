using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: ������������� � GameDB
public class GameSettings : MonoBehaviour
{
    private GameHub _gameHub;
    private GameData _gameData;
    private SaveLoadEngine _saveLoadEngine;
    [SerializeField] private AudioClip _musicBackground;//����� ����� ��� 
    [SerializeField] private float _soundVolume = 0.6f;
    [SerializeField] private List<UnitConfig> _configEnemyUnit = new();
    [SerializeField] private List<UnitConfig> _configFriendUnit = new();
    [SerializeField, Tooltip("������� ��������")] private Sprite[] _spriteLevel;
    [SerializeField, Tooltip("��������� ���������� ��������")] private int _brahmin = 10;
    [SerializeField, Tooltip("��������� �������� �����")] private int _wave = 0;
    [SerializeField, Tooltip("��������� ���������� �����")] private int _startCoins = 140;
    private int _maxCheatMoney = 10000;
    private List<UnitComponent> _friendsActive = new();
    public IReadOnlyList<UnitConfig> GetFriendsConfigs => _configFriendUnit;
    public IReadOnlyList<UnitConfig> GetEnemiesConfigs => _configEnemyUnit;
    public IReadOnlyList<UnitComponent> GetFriendsActive => _friendsActive;
    /// <summary>
    /// �������� ������������ ������� �����
    /// </summary>
    public int GetMaxLevel => _spriteLevel.Length;
    /// <summary>
    /// �������� ���������� ����� ��� ������ ����
    /// </summary>
    public int GetStartCoins => _gameData.Coins;

    public GameData GetGameData => _gameData;



    /// <summary>
    /// �������� ������� ��������� ��� SFX
    /// </summary>
    public float GetSoundValue => _soundVolume;

    public void Initialized(GameHub gameHub)
    {
        Time.timeScale = 1f;
        _gameHub = gameHub;

        if (GameState.Instance.IsLoading)
        {
            Debug.Log("�������� gameData ��  GameState.");
            _gameData = GameState.Instance.LoadedGameData;

        }
        else
        {
            Debug.Log("������������� ����� ������");
            _gameData = new GameData(_brahmin, _wave, _startCoins);
        }

        _saveLoadEngine = new SaveLoadEngine();

        SoundEngine.Instance.PlaySound(_musicBackground, SoundType.Music);
        SoundEngine.Instance.SetVolume(0.6f, SoundType.Music);
    }

    /// <summary>
    /// �������� ��� ����� ��� ����������� ���������� ��� 
    /// </summary>
    /// <param name="unit"></param>
    public void AddUnit(UnitComponent unit)
    {

        _friendsActive.Add(unit);
    }

    public void RemoveUnit(UnitComponent unit)
    {

        if (_friendsActive.Contains(unit)) _friendsActive.Remove(unit);
    }

    public void UpdateLevelUnit()
    {
        if (_gameData == null)
        {
            Debug.LogError("_gameData �� ���������������!");
            return;
        }
        if (_gameData.UnitsData == null)
        {
            Debug.LogError("_gameData.UnitsData �� ���������������!");
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

            // ��������� ��� ��������� ������ � ������� � ������ �����
            _gameData.UnitsData[unitId][cellPosition] = unitLevel;
        }





    }

    public void SaveGame()
    {
        _gameData.Brahmin = _gameHub.GetBrahmin.GetBrahminList.Count;
        _gameData.Wave = _gameHub.GetWaveEngine.GetWaveNumber;
        _gameData.Coins = _gameHub.GetWalletEngine.GetWallet.Coins;
        UpdateLevelUnit();

        _saveLoadEngine.SaveData(_gameData);
    }



    public Sprite GetSpriteLevel(int index)
    {
        if (index - 1 < _spriteLevel.Length)
        {
            return _spriteLevel[index - 1];
        }

        Debug.LogError($"������ {index} � sprite level.");
        return null;
    }



    public void LoadSceneAsync(string sceneName)
    {

        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }


    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!asyncOperation.isDone)
        {

            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            Debug.Log(sceneName + ": " + (progress * 100) + "%");
            if (progress * 100 == 100)
            {

                Debug.Log("����� " + sceneName + " ��������� ���������.");
            }
            yield return null;
        }


    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            if ( _maxCheatMoney >= _gameHub.GetWalletEngine.GetWallet.Coins )
                _gameHub.GetWalletEngine.MoreMoney(500);
        }
    }
}
