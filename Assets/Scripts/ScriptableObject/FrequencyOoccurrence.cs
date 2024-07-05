using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frequency", menuName = "Configuration/Frequency of Ooccurrence", order = 1)]
public class FrequencyOoccurrence : ScriptableObject
{
    [SerializeField, Tooltip(" + ���������� ������ �� ����� �����")]
    private int _addEnemy;

    [SerializeField, Tooltip("���������� ��������� ������ � 1�� �����")]
    private int _numberEnemiesInWave = 5;
  
    [SerializeField, Tooltip("������ ����� ��������� �����")] 
    private float _timeNewWave = 15.0f;

    [SerializeField]
    private List<EnemyConfiguration> _enemy = new();

    public IReadOnlyList<EnemyConfiguration> GetEnemyList => _enemy;
    /// <summary>
    /// ���������� ����������� ������ � ������ ����� ����� 
    /// </summary>
    public int GetAddEnemy => _addEnemy;
    /// <summary>
    /// ���������� ������ ����������� � ����� �����
    /// </summary>
    public int GetNumberEnemiesInWave => _numberEnemiesInWave;
    /// <summary>
    /// ������ �� ����� �����
    /// </summary>
    public float GetTimeNewWave => _timeNewWave;    
}

[Serializable]
public class EnemyConfiguration
{
   

    [ SerializeField] private UnitConfig _enemyConfig;

    [SerializeField, Tooltip("������� ��������� �����"), Range(0f, 100f)]
    private float _percentageAppearance;
    [SerializeField, Tooltip("��� ������������� ���� ��������� � ������ ����� ������"), Range(0f, 100f)]
    private float _percentageRatio;
 
   public UnitConfig GetEnemyConfig => _enemyConfig;
    ///// <summary>
    ///// ������� ��������� �����
    ///// </summary>
    //public float GetPercentageAppearance => _percentageAppearance;
  
    public List<float> GetDataPercentage => new List<float> { _percentageAppearance , _percentageRatio };

    /// <summary>
    /// ����������� ���� ��������� � ������ ����� ������
    /// </summary>
    //public void IncreasePercentageAppearance()
    //{

    //    _percentageAppearance +=  _percentageRatio;

    //}
}
