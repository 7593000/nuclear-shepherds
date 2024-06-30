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

    [SerializeField, Tooltip( "������� ��������� �����" ), Range( 0f , 100f )]
    private float _percentageAppearance;
    [SerializeField, Tooltip( "��� ������������� ���� ��������� � ������ ����� ������" ), Range( 0f , 100f )]
    private float _percentageRatio;

    public UnitConfig GetEnemyConfig => _enemyConfig;
    /// <summary>
    /// ������� ��������� �����
    /// </summary>
    public float GetPercentageAppearance => _percentageAppearance;
    /// <summary>
    /// ������ ����� ��������� � ������ ����� ������
    /// </summary>
    public void GetPercentageRatio()
    {
       
        _percentageAppearance += _percentageRatio;
        
      
    }
}
