
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Волны противников
/// </summary>
public class WaveEngine : MonoBehaviour
{
    public event Action<int> OnWave;


    [SerializeField] private FrequencyOoccurrence _config;
    /// <summary>
    /// Текущий счет волны
    /// </summary>
    private int _waveNumber = 0;
    //_waveNumbler * шаг увеличения процента появления у врага  + шанс появления врага .
 
    [SerializeField, Tooltip("таймер новой генерации волны")] private float _timeNewWave = 5.0f; 
    [SerializeField,Tooltip("Точки для респавна противников")] private Transform[] _startEnemyPosition;

    private List<UnitConfig> _enemyList = new(); 

    public void Initialized(GameHub gameHub)
    {
        OnWave?.Invoke(_waveNumber);
    }


       
    private void CreateEnemyUnits()
    {

    }

    /// <summary>
    /// Генерация волны
    /// </summary>
    private void WaveGeneration()
    {

    }


    private void StartWave()
    {

    }
    


}
