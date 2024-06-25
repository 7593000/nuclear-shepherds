using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameHub : MonoBehaviour
{
    [SerializeField]
    private UnitsUpdateEngine _unitsEngine;
     
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
    private void Awake()
    {
        _unitsEngine ??= FindAnyObjectByType<UnitsUpdateEngine>(); 
        _points??= FindAnyObjectByType<PointsTargerEngine>(); 
        _bottomPanel??= FindFirstObjectByType<BottomPanel>();  
    }

    private void Start()
    {
        _bottomPanel.Initialized(this);
    }

    public static void Logger(string txt)
    {
        Debug.Log( txt );
    }
}
