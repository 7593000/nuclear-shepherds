using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frequency", menuName = "Configuration/Frequency of Ooccurrence", order = 1)]
public class FrequencyOoccurrence : ScriptableObject
{

    [SerializeField]
    private List<EnemyConfiguration> _enemy = new();




   [Serializable]
struct  EnemyConfiguration 
    {

        [SerializeField] UnitConfig[] _enemyConfig;

        [SerializeField, Tooltip("������� ��������� �����"), Range(0f, 100f)] private float _percentageAppearance;
        [SerializeField, Tooltip("��� ������������� ���� ��������� � ������ ����� ������"), Range(0f, 100f)] private float _percentageRatio;
  
    
    }
}
