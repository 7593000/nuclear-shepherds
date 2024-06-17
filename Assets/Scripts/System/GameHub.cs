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


    public UnitsUpdateEngine GetUnitsUpdateEngine => _unitsEngine;
    public PointsTargerEngine GetPointsTarget => _points; 
    public BrahminManager GetBrahmin => _brahmin;   
    private void Awake()
    {
        _unitsEngine ??= FindAnyObjectByType<UnitsUpdateEngine>(); 
        _points??= FindAnyObjectByType<PointsTargerEngine>(); 
    }
}
