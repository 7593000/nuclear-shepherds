using UnityEngine;


public class GameHub : MonoBehaviour
{
    [SerializeField]
    private WaveEngine _waveEngine;
    [SerializeField]
    private UnitsUpdateEngine _unitsEngine;
    [SerializeField]
    private WalletEngine _walletEngine;
    [SerializeField]
    private PointsTargerEngine _points;
    [SerializeField]
    private CreatePoolEnemy _poolEnemy;
    [SerializeField]
    private BrahminManager _brahminEngine;
    [SerializeField]
    private BottomPanel _bottomPanel;
    [SerializeField]
    private TileMapEngine _tileMapEngine;

    public UnitsUpdateEngine GetUnitsUpdateEngine => _unitsEngine;
    public PointsTargerEngine GetPointsTarget => _points;
    public BrahminManager GetBrahmin => _brahminEngine;
    public TileMapEngine GetTileMap => _tileMapEngine;
    public WalletEngine GetWalletEngine => _walletEngine;
    public WaveEngine GetWaveEngine => _waveEngine;
   
    private void Awake()
    {
        _waveEngine ??= GetComponent<WaveEngine>();
        _unitsEngine ??= GetComponent<UnitsUpdateEngine>();
        _points ??= GetComponent<PointsTargerEngine>();
        _bottomPanel ??= GetComponent<BottomPanel>();
        _walletEngine ??= GetComponent<WalletEngine>();
    }

    private void Start()
    {
        _walletEngine.Initialized(100);
        _bottomPanel.Initialized(this);
        _brahminEngine.Initialized(this);
        _waveEngine.Initialized(this);

    }

    //Todo=> TEMP
    public static void Logger(string txt)
    {
        Debug.Log(txt);
    }
}
