using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frequency", menuName = "Configuration/Frequency of Ooccurrence", order = 1)]
public class FrequencyOoccurrence : ScriptableObject
{
    [SerializeField, Tooltip(" + количество юнитов на новую волну")]
    private int _addEnemy;

    [SerializeField, Tooltip("Количество вражеских юнитов в 1ой волне")]
    private int _numberEnemiesInWave = 5;
  
    [SerializeField, Tooltip("таймер новой генерации волны")] 
    private float _timeNewWave = 15.0f;

    [SerializeField]
    private List<EnemyConfiguration> _enemy = new();

    public IReadOnlyList<EnemyConfiguration> GetEnemyList => _enemy;
    /// <summary>
    /// количество добавляемых юнитов в каждую новую волну 
    /// </summary>
    public int GetAddEnemy => _addEnemy;
    /// <summary>
    /// Количество юнитов добавляемых в новой волне
    /// </summary>
    public int GetNumberEnemiesInWave => _numberEnemiesInWave;
    /// <summary>
    /// Таймер до новой волны
    /// </summary>
    public float GetTimeNewWave => _timeNewWave;    
}

[Serializable]
public class EnemyConfiguration
{
   

    [ SerializeField] private UnitConfig _enemyConfig;

    [SerializeField, Tooltip("Процент появления юнита"), Range(0f, 100f)]
    private float _percentageAppearance;
    [SerializeField, Tooltip("шаг увеличивающий шанс появления с каждой новой волной"), Range(0f, 100f)]
    private float _percentageRatio;
 
   public UnitConfig GetEnemyConfig => _enemyConfig;
    ///// <summary>
    ///// Процент появления юнита
    ///// </summary>
    //public float GetPercentageAppearance => _percentageAppearance;
  
    public List<float> GetDataPercentage => new List<float> { _percentageAppearance , _percentageRatio };

    /// <summary>
    /// Увеличивает шанс появления с каждой новой волной
    /// </summary>
    //public void IncreasePercentageAppearance()
    //{

    //    _percentageAppearance +=  _percentageRatio;

    //}
}
