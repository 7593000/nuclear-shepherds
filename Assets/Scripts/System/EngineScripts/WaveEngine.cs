
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Волны противников
/// </summary>
public class WaveEngine : MonoBehaviour
{
    public event System.Action<int> OnWave; //TODO => убрать систем.. переделать Random

    private GameHub _gameHub;
    [SerializeField] private FrequencyOoccurrence _config;

    [SerializeField, Tooltip( " Текущая волна" )] private int _waveNumber = 0;

    [SerializeField, Tooltip( "Количество вражеских юнитов в 1ой волне" )]
    private int _numberEnemiesInWave = 5;

    private List<Enemy> _enemiesWave = new();

    //_waveNumbler * шаг увеличения процента появления у врага  + шанс появления врага .

    [SerializeField, Tooltip( "таймер новой генерации волны" )] private float _timeNewWave = 15.0f;
    [SerializeField, Tooltip( "Точки для респавна противников" )] private Transform[] _startEnemyPosition;

    [SerializeField] private List<UnitComponent> _enemyList = new();

    public void Initialized( GameHub gameHub )
    {
        _gameHub = gameHub;
        WaveGeneration();
        OnWave?.Invoke( _waveNumber );
    }


    /// <summary>
    /// Составляение волны юнитами. 
    /// </summary>
    private void CreateEnemyUnits()
    {
        _enemyList.Clear();
        int count = 0;
        while ( count < _numberEnemiesInWave )
        {

            foreach ( EnemyConfiguration enemyConfig in _config.GetEnemyList )
            {
                if ( GetRange( enemyConfig.GetPercentageAppearance ) )
                {

                    UnitComponent enemyUnit = _gameHub.GetPoolEnemy.GetEnemy( enemyConfig.GetEnemyConfig );
                    if ( enemyUnit.TryGetComponent<Enemy>( out Enemy enemy ) )
                    {
                        enemyUnit.GetComponent<Enemy>().BusyWave = true;
                        _enemyList.Add( enemyUnit );
                        count++;
                    }else
                    {
                        Debug.Log( "Нет Enemy компонетна" );
                    }
                }
            }


        }
    }

    /// <summary>
    /// Генерация волны
    /// </summary>
    private void WaveGeneration()
    {
        CreateEnemyUnits();
        StartWave();
    }


    private void StartWave()
    {
        StartCoroutine( StartSpawnEnemy() );
    }

    private IEnumerator StartSpawnEnemy()
    {
        foreach ( UnitComponent enemy in _enemyList )
        {
            enemy.transform.position = _startEnemyPosition[ 1 ].transform.position;
            enemy.gameObject.SetActive( true );
            yield return new WaitForSeconds( 0.5f );
        }

    }

    private bool GetRange( float value )
    {
        int indexRandom = Random.Range( 0 , 100 );

        return value > indexRandom;

    }

}
