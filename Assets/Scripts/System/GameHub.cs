using UnityEngine;


public class GameHub : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [Space] 
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
    public PoolEnemy GetPoolEnemy => _poolEnemy ;
    public WindowInfoUnit GetWindowInfoUnit => _windowInfoUnit;
    private void Awake()
    {
        _gameSettings??= GetComponent<GameSettings>();   
        _waveEngine ??= GetComponent<WaveEngine>();
        _unitsEngine ??= GetComponent<UnitsUpdateEngine>();
        _points ??= GetComponent<PointsTargerEngine>();
        _bottomPanel ??= GetComponent<BottomPanel>();
        _walletEngine ??= GetComponent<WalletEngine>();
        _poolEnemy??= GetComponent<PoolEnemy>();
        _windowInfoUnit??= FindFirstObjectByType<WindowInfoUnit>();
    }

    private void Start()
    {
        if(GameState.Instance.IsLoading )
        { Debug.Log( "LOAD" ); }
        _gameSettings.Initialized();
        _walletEngine.Initialized(_gameSettings.GetStartCoins);
        _poolEnemy.Initialized( this );
        _bottomPanel.Initialized(this);
        _brahminEngine.Initialized(this);
        _waveEngine.Initialized(this);

    }

    //Todo=> TEMP
    public static void Logger(string txt)
    {
        Debug.Log(txt);
    }
    [ContextMenu( "Save Game" )]
    public void SaveGame()
    { }


    }
