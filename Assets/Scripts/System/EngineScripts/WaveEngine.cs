
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>
/// ����� �����������
/// </summary>
public class WaveEngine : MonoBehaviour
{
    public event System.Action<int> OnWave; //TODO => ������ ������.. ���������� Random

    private GameHub _gameHub;
    [SerializeField] private FrequencyOoccurrence _config;

    private Dictionary<UnitConfig, List<float>> _unitConfigData = new();

    private Coroutine _coroutineCreateUnit;
    private Coroutine _coroutineStartWave;
    [SerializeField, Tooltip(" ������� �����")] private int _waveNumber = 1;

    [SerializeField, Tooltip("���������� ��������� ������ � 1�� �����")]
    private int _numberEnemiesInWave = 5;

    private List<Enemy> _enemiesWave = new();

    //_waveNumbler * ��� ���������� �������� ��������� � �����  + ���� ��������� ����� .

    [SerializeField, Tooltip("������ ����� ��������� �����")] private float _timeNewWave = 15.0f;
    [SerializeField, Tooltip("����� ��� �������� �����������")] private Transform[] _startEnemyPosition;
    [SerializeField] private List<UnitComponent> _enemyList = new();


    public void Initialized(GameHub gameHub)
    {
        _gameHub = gameHub;

        CreateDictionaryData();
       
        WaveGeneration();

        OnWave?.Invoke(_waveNumber);
    }

    /// <summary>
    /// ��������� ������ �� SO 
    /// </summary>
    private void CreateDictionaryData()
    {

        foreach (EnemyConfiguration enemyConfig in _config.GetEnemyList)
        {
            _unitConfigData[enemyConfig.GetEnemyConfig] = enemyConfig.GetDataPercentage;

        }

    }

    /// <summary>
    /// ��� �������� ������ �� ���������� . ��������� � ���������
    /// </summary>
    public void LoadData()
    {
        _waveNumber = 1;
        _numberEnemiesInWave += _config.GetAddEnemy * _waveNumber;



    }

    /// <summary>
    /// ������������ ����� �������. 
    /// </summary>
    private void CreateEnemyUnits()
    {
        
        _enemyList.Clear();



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
                    UnitComponent enemyUnit = _gameHub.GetPoolEnemy.GetEnemy(configUnit);
                   
                    if (enemyUnit.TryGetComponent(out Enemy unit))
                    {
                        unit.BusyWave = true;
                        _enemyList.Add(unit);
                        count++;
                    }
                    else
                    {
                        Debug.Log("��� Enemy ����������");
                    }
                }


            }


        }
    }

    /// <summary>
    /// ��������� �����
    /// </summary>
    private void WaveGeneration()
    {

        CreateEnemyUnits();
        StartWave();
    }


    private void StartWave()
    {
        if (_coroutineCreateUnit != null)
        {
            StopCoroutine(_coroutineCreateUnit);
            _coroutineCreateUnit = null;
        }
        _coroutineCreateUnit = StartCoroutine(StartSpawnEnemy());

        if (_coroutineStartWave != null) return;

        _coroutineStartWave = StartCoroutine(StartNewWave());
    }

    private IEnumerator StartNewWave()
    {
        while (true)
        {

            yield return new WaitForSeconds(_timeNewWave);

            _waveNumber++;
            _numberEnemiesInWave += _config.GetAddEnemy;
         
            UpdateEnemyAppearances();
            WaveGeneration();

            OnWave?.Invoke(_waveNumber);


        }


    }



    private IEnumerator StartSpawnEnemy()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {

            _enemyList[i].transform.position = _startEnemyPosition[1].transform.position;
            _enemyList[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

    }
    /// <summary>
    /// ���������� ����������� ��������� ������
    /// </summary>
    private void UpdateEnemyAppearances()
    {
        foreach (var enemy in _unitConfigData)
        {
            List<float> values = enemy.Value;
            values[0] += values[1]; // ����������� ������� ��������� �� �������� ����
        }
    }
    private bool GetRange(float value)
    {
        int indexRandom = Random.Range(0, 100);

        return indexRandom < value;

    }

}
