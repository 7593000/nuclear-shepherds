using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameHub : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [Space]
    [SerializeField]
    private UnitPositionEngine _unitPositionEngine;
    [SerializeField]
    private WaveEngine _waveEngine;
    [SerializeField]
    private UnitsUpdateEngine _unitsEngine;
    [SerializeField]
    private WalletEngine _walletEngine;
    [SerializeField]
    private PointsTargerEngine _points;
    [SerializeField]
    private PoolEnemy _poolEnemy;
    [SerializeField]
    private PoolExplosion _poolExplosion;
    [SerializeField]
    private BrahminManager _brahminEngine;
    [SerializeField]
    private BottomPanel _bottomPanel;
    [SerializeField]
    private WindowInfoUnit _windowInfoUnit;
    [SerializeField]
    private TileMapEngine _tileMapEngine;



    public GameSettings GetGameSettings => _gameSettings;
    public UnitsUpdateEngine GetUnitsUpdateEngine => _unitsEngine;
    public PointsTargerEngine GetPointsTarget => _points;
    public BrahminManager GetBrahmin => _brahminEngine;
    public TileMapEngine GetTileMap => _tileMapEngine;
    public WalletEngine GetWalletEngine => _walletEngine;
    public WaveEngine GetWaveEngine => _waveEngine;
    public PoolEnemy GetPoolEnemy => _poolEnemy;
    public PoolExplosion GetPoolExplosion => _poolExplosion;
    public WindowInfoUnit GetWindowInfoUnit => _windowInfoUnit;


    private void Awake()
    {
        _unitPositionEngine ??= GetComponent<UnitPositionEngine>();
        _gameSettings ??= GetComponent<GameSettings>();
        _walletEngine ??= GetComponent<WalletEngine>();
        _waveEngine ??= GetComponent<WaveEngine>();
        _unitsEngine ??= GetComponent<UnitsUpdateEngine>();
        _points ??= GetComponent<PointsTargerEngine>();
        _bottomPanel ??= FindFirstObjectByType<BottomPanel>();
        _walletEngine ??= GetComponent<WalletEngine>();
        _poolEnemy ??= GetComponent<PoolEnemy>();
        _windowInfoUnit ??= FindFirstObjectByType<WindowInfoUnit>();




    }

    private void InitializeComponents()
    {
        CleanupComponents();

        _gameSettings.Initialized(this);
        _walletEngine.Initialized(this);
        _poolEnemy.Initialized(this);
        _bottomPanel.Initialized(this);
        _brahminEngine.Initialized(this);
        _waveEngine.Initialized(this);

        _unitPositionEngine.Initialized(this);

       
          
            GameState.Instance.IsLoading = false;
    }
  
   
    
    private void OnEnable()
    {
        InitializeComponents();
    }

    private void OnDisable()
    {
        CleanupComponents();
    }

    private void CleanupComponents()
    {


        _bottomPanel.Cleanup();
        _walletEngine.Cleanup();
    }

    public void ReloadData()
    {
        LoadSceneAsync("LoadGame");

    }
    //Todo=> TEMP
    public static void Logger(string txt) => Debug.Log(txt);
    //TEMP

    public void LoadSceneAsync(string sceneName)
    {

        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }
    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {

            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            Debug.Log(sceneName + ": " + (progress * 100) + "%");
            if (progress * 100 == 100)
            {

                Debug.Log("Сцена " + sceneName + " Загружена полностью.");
            }
            yield return null;
        }


    }


}
