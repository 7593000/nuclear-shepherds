
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>
/// Волны противников
/// </summary>
public sealed class WaveEngine : MonoBehaviour
{
    public event System.Action<int> OnWave; //TODO => убрать систем.. переделать Random

    private GameHub _gameHub;
    [SerializeField] private FrequencyOoccurrence _config;

    private Dictionary<UnitConfig, List<float>> _unitConfigData = new();
    private Dictionary<UnitConfig, bool> _interestStatus = new  ();

    private List<Enemy> _enemiesWave = new();

    private Coroutine _coroutineCreateUnit;
    private Coroutine _coroutineStartWave;

    [SerializeField, Tooltip(" Текущая волна")] private int _waveNumber = 1;

   [SerializeField, Tooltip("Количество вражеских юнитов в  волне")]
   private int _numberEnemiesInWave;
   


    [SerializeField, Tooltip("Точки для респавна противников")] private Transform[] _startEnemyPosition;
    [SerializeField] private List<Enemy> _enemyList = new();


    public void Initialized(GameHub gameHub)
    {
        _gameHub = gameHub;
        InitializeComponents();
    }


    private void InitializeComponents()
    {
        StopAllCoroutines();

        _waveNumber = _gameHub.GetGameSettings.GetGameData.Wave;

        Debug.Log("текущая волна установлена на: " + _waveNumber);
        _numberEnemiesInWave = _config.GetNumberEnemiesInWave + (_config.GetAddEnemy * _waveNumber);
        Debug.Log("Количество вражеских юнитов на волне : " + _numberEnemiesInWave);
        

        CreateDictionaryData();


        for (int i = 0; i < _waveNumber; i++)
        {
            UpdateEnemyAppearances();
        }



        OnWave?.Invoke(_waveNumber);
        WaveGeneration();

        

    }



    /// <summary>
    /// Перегнать данные из SO 
    /// </summary>
    private void CreateDictionaryData()
    {

        foreach (EnemyConfiguration enemyConfig in _config.GetEnemyList)
        {
            _unitConfigData[enemyConfig.GetEnemyConfig] = enemyConfig.GetDataPercentage;
            _interestStatus[enemyConfig.GetEnemyConfig] = true;
        }

    }
 

    /// <summary>
    /// Составляение волны юнитами. 
    /// </summary>
    private void CreateEnemyUnits()
    {
        
        //_enemyList.Clear();



        int count = 0;

        while (count < _numberEnemiesInWave)
        {
            foreach (KeyValuePair<UnitConfig, List<float>> enemy in _unitConfigData)
            {
                UnitConfig configUnit = enemy.Key;
                List<float> values = enemy.Value;
                float percentageAppearance = values[0];


                if (GetRange(percentageAppearance))
                {
                    Enemy enemyUnit = _gameHub.GetPoolEnemy.GetEnemy(configUnit);
                    enemyUnit.BusyWave = true;
                    _enemyList.Add(enemyUnit);
                    count++;
                    
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
        //if (_coroutineCreateUnit != null)
        //{
        //    StopCoroutine(StartSpawnEnemy());
        //    _coroutineCreateUnit = null;
        //}
        //_coroutineCreateUnit = StartCoroutine(StartSpawnEnemy());

        if (_coroutineStartWave != null) return;

        _coroutineStartWave = StartCoroutine(StartNewWave());
    }

    private IEnumerator StartNewWave()
    {
        
        
        yield return new WaitForSeconds(_config.GetTimeNewWave);

        //todo=> ADD SOUND START WAVE

            StartCoroutine(StartSpawnEnemy());

            _waveNumber++;
            _numberEnemiesInWave += _config.GetAddEnemy;
         
            UpdateEnemyAppearances();
            WaveGeneration();

            OnWave?.Invoke(_waveNumber);
             
        
         
    }



    private IEnumerator StartSpawnEnemy()
    {

        // Спавн врагов
        for (int i = 0; i < _enemyList.Count; i++)
        {
            _enemyList[i].transform.position = _startEnemyPosition[1].transform.position;
            _enemyList[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        // Проверка активности врагов
        while (_enemyList.Count > 0)
        {
            for (int i = _enemyList.Count - 1; i >= 0; i--)
            {
                if (!_enemyList[i].gameObject.activeSelf)
                {
                    _enemyList.Remove(_enemyList[i]);
                  
                   
                }
            }
            yield return new WaitForSeconds(1f);
        }

        yield return StartCoroutine(StartNewWave());

    }
    /// <summary>
    /// Обновление вероятности появления врагов
    /// </summary>
    private void UpdateEnemyAppearances()
    {
        foreach (var enemy in _unitConfigData)
        {
            UnitConfig enemyConfig = enemy.Key;
            List<float> values = enemy.Value;
            float percentage = values[0];
            float step = values[1];
            
         // Увеличиваем процент появления на значение шага
            if (_interestStatus[enemyConfig])
            {
                percentage += step;
                if(percentage>=100f)
                {
                    percentage = 100;
                    _interestStatus[enemyConfig] = false; //Уменьшаем
                    
                }
            }
            else
            {
                percentage -= step;
                if(percentage<= 50f)
                {
                    percentage = 50f;
                    _interestStatus[enemyConfig] = true;  //Увеличиваем
                    
                }
            }
            values[0] = percentage;
           
        }

    }
    private bool GetRange(float value)
    {
        int indexRandom = Random.Range(0, 100);

        return indexRandom < value;

    }

}
