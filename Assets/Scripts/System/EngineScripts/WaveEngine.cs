
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
        WaveGeneration();
        OnWave?.Invoke(_waveNumber);
    }


    /// <summary>
    /// ��� �������� ������ �� ���������� . ��������� � ���������
    /// </summary>
    public void LoadData()
    {
        _waveNumber = 1;
        _numberEnemiesInWave += _config.GetAddEnemy * _waveNumber;
      
        foreach (EnemyConfiguration enemyConfig in _config.GetEnemyList)
        {
            for(int i = 0;i< _waveNumber;i++)
            {
                enemyConfig.GetPercentageRatio();
            }
          
        }

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
            foreach (EnemyConfiguration enemyConfig in _config.GetEnemyList)
            {
               

                bool status = GetRange(enemyConfig.GetPercentageAppearance);
                Debug.Log(enemyConfig.GetPercentageAppearance);
                if (status)
                {
                    UnitComponent enemyUnit = _gameHub.GetPoolEnemy.GetEnemy(enemyConfig.GetEnemyConfig);
                  
                    if (enemyUnit.TryGetComponent(out Enemy enemy))
                    {
                        enemyUnit.GetComponent<Enemy>().BusyWave = true;
                        _enemyList.Add(enemyUnit);
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
      //  StartCoroutine(StartSpawnEnemy());
        StartCoroutine(StartNewWave());
    }

    private IEnumerator StartNewWave()
    {
        while (true)
        {
            StartCoroutine(StartSpawnEnemy());
            yield return new WaitForSeconds(_timeNewWave);
          
            _waveNumber++;

            _numberEnemiesInWave += _config.GetAddEnemy;
            
            OnWave?.Invoke(_waveNumber);

            foreach (EnemyConfiguration enemyConfig in _config.GetEnemyList)
            {
                Debug.Log(enemyConfig.GetPercentageAppearance);
                enemyConfig.IncreasePercentageAppearance();
                Debug.Log(enemyConfig.GetPercentageAppearance);
            }

                CreateEnemyUnits();
            
        }
      

    }


    private IEnumerator StartSpawnEnemy()
    {
        for(int i =0; i< _enemyList.Count; i++)
        {

            _enemyList[i].transform.position = _startEnemyPosition[1].transform.position;
            _enemyList[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

    }

    private bool GetRange(float value)
    {
        int indexRandom = Random.Range(0, 100);

        return indexRandom < value;

    }

}
