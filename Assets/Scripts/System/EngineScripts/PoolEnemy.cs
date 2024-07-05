using System.Collections.Generic;
using UnityEngine;

public class PoolEnemy : MonoBehaviour
{
    private GameHub _gameHub;

    [SerializeField] private Transform _parent;
    [SerializeField] private int _countEnemy = 20;
    private Dictionary<UnitConfig , List<UnitComponent>> _pool = new();
    [SerializeField] private List<UnitComponent> _poolEnemy = new List<UnitComponent>();
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
            _pool.Add( enemy , new List<UnitComponent>() );
            
            for ( int i = 0; i < count; i++ )
            {
                InstantiateEnemy( enemy );
            }
        }
    }

    /// <summary>
    /// Создать врага и добавить его в пул
    /// </summary>
    /// <param name="unitConfig">Конфигурация юнита</param>
    /// <returns>Созданный юнит</returns>
    private UnitComponent InstantiateEnemy( UnitConfig unitConfig )
    {
        UnitComponent enemyUnit = Instantiate( unitConfig.GetPrefab , _parent );
        enemyUnit.gameObject.SetActive( false );
        enemyUnit.Container( _gameHub );
        _pool[ unitConfig ].Add( enemyUnit );  // Перенос добавления в пул здесь
        _poolEnemy.Add( enemyUnit );
       
        return enemyUnit;
    }

    /// <summary>
    /// Взять врага из пула
    /// </summary>
    /// <param name="config">Конфигурация юнита</param>
    /// <returns>Юнит из пула</returns>
    public UnitComponent GetEnemy( UnitConfig config )
    {
        if ( _pool.TryGetValue( config , out List<UnitComponent> tempList ) )
        {
            foreach ( UnitComponent unit in tempList )
            {
                if ( unit.TryGetComponent( out Enemy enemy ) )
                {
                    if ( !enemy.BusyWave && !enemy.gameObject.activeSelf )  // Изменение проверки
                    {
                        return unit;
                    }
                }
            }
        }
        else
        {
            Debug.Log("ERROR No COnfig");
        }

        // Если не найден свободный юнит, создаем новый
        UnitComponent tempUnit = InstantiateEnemy( config );
        return tempUnit;
    }
}
