using UnityEngine;


public class GameHub : MonoBehaviour
{
    [SerializeField]
    private UnitsUpdateEngine _unitsEngine;
    [SerializeField]
    private WalletEngine _walletEngine;
    [SerializeField]
    private PointsTargerEngine _points;
    [SerializeField]
    private CreatePoolEnemy _poolEnemy;
    [SerializeField]
    private BrahminManager _brahmin;
    [SerializeField]
    private BottomPanel _bottomPanel;
    [SerializeField]
    private TileMapEngine _tileMapEngine;

    public UnitsUpdateEngine GetUnitsUpdateEngine => _unitsEngine;
    public PointsTargerEngine GetPointsTarget => _points;
    public BrahminManager GetBrahmin => _brahmin;
    public TileMapEngine GetTileMap => _tileMapEngine;
    public WalletEngine GetWalletEngine => _walletEngine;

    private void Awake()
    {
        _unitsEngine ??= FindAnyObjectByType<UnitsUpdateEngine>();
        _points ??= FindAnyObjectByType<PointsTargerEngine>();
        _bottomPanel ??= FindFirstObjectByType<BottomPanel>();
        _walletEngine??=FindFirstObjectByType<WalletEngine>();
    }

    private void Start()
    {
        _walletEngine.Initialized(100);
        _bottomPanel.Initialized(this);
        _brahmin.Initialized();


    }

    //Todo=> TEMP
    public static void Logger(string txt)
    {
        Debug.Log(txt);
    }
}
