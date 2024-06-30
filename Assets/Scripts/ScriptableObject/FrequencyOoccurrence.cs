using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Frequency" , menuName = "Configuration/Frequency of Ooccurrence" , order = 1 )]
public class FrequencyOoccurrence : ScriptableObject
{

    [SerializeField]
    private List<EnemyConfiguration> _enemy = new();

    public IReadOnlyList<EnemyConfiguration> GetEnemyList => _enemy;

}

[Serializable]
public struct EnemyConfiguration
{

    [SerializeField] private UnitConfig _enemyConfig;

    [SerializeField, Tooltip( "Процент появления юнита" ), Range( 0f , 100f )]
    private float _percentageAppearance;
    [SerializeField, Tooltip( "щаг увеличивающий шанс появления с каждой новой волной" ), Range( 0f , 100f )]
    private float _percentageRatio;

    public UnitConfig GetEnemyConfig => _enemyConfig;
    /// <summary>
    /// Процент появления юнита
    /// </summary>
    public float GetPercentageAppearance => _percentageAppearance;
    /// <summary>
    /// Правка шанса появления с каждой новой волной
    /// </summary>
    public void GetPercentageRatio()
    {
       
        _percentageAppearance += _percentageRatio;
        
      
    }
}
