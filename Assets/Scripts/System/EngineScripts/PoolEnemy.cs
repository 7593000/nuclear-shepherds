using System.Collections.Generic;
using UnityEngine;

public class PoolEnemy : MonoBehaviour
{

    private GameHub _gameHub;


    [SerializeField] private Transform _parent;
    [SerializeField] private int _countEnemy = 20;
    private Dictionary<UnitConfig , List<UnitComponent>> _pool = new();

    public void Initialized( GameHub gameHub )
    {
        _gameHub = gameHub;


        CreatePoolEnemy( _countEnemy );
    }

    /// <summary>
    /// Собрать пул для вражеских юнитов
    /// </summary>
    private void CreatePoolEnemy( int count )
    {


        foreach ( UnitConfig enemy in _gameHub.GetGameData.GetEnemiesConfigs )
        {
            _pool.Add(enemy, new List<UnitComponent>());

            for ( int i = 0; i < count; i++ )
            {

               

                _pool[ enemy ].Add( InstantiateEnemy( enemy ) );

            }

        }
    }
    private UnitComponent InstantiateEnemy( UnitConfig unitConfig )
    {
        UnitComponent enemyUnit = Instantiate( unitConfig.GetPrefab , _parent );
        enemyUnit.gameObject.SetActive( false );
        enemyUnit.Container( _gameHub );
        
        return enemyUnit;
    }


    /// <summary>
    /// Взять врага из пула
    /// </summary>
    /// <returns></returns>
    public UnitComponent GetEnemy( UnitConfig config )
    {
        UnitComponent tempUnit = null;
        if ( _pool.TryGetValue( config , out List<UnitComponent> tempList ) )
        {
            foreach ( UnitComponent unit in tempList )
            {
                if (  unit.TryGetComponent( out Enemy enemy) )
             
                {
                    if(!enemy.BusyWave)
                    {
                        tempUnit = unit;
                        break;
                    }
                    

                }
            }


        }
        if ( tempUnit == null )
        {
            tempUnit = InstantiateEnemy( config );
            _pool[ config ].Add( tempUnit );


        }
        return tempUnit;
    }
}
