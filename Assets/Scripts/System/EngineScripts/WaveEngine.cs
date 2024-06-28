
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����� �����������
/// </summary>
public class WaveEngine : MonoBehaviour
{
    public event Action<int> OnWave;


    [SerializeField] private FrequencyOoccurrence _config;
    /// <summary>
    /// ������� ���� �����
    /// </summary>
    private int _waveNumber = 0;
    //_waveNumbler * ��� ���������� �������� ��������� � �����  + ���� ��������� ����� .
 
    [SerializeField, Tooltip("������ ����� ��������� �����")] private float _timeNewWave = 5.0f; 
    [SerializeField,Tooltip("����� ��� �������� �����������")] private Transform[] _startEnemyPosition;

    private List<UnitConfig> _enemyList = new(); 

    public void Initialized(GameHub gameHub)
    {
        OnWave?.Invoke(_waveNumber);
    }


       
    private void CreateEnemyUnits()
    {

    }

    /// <summary>
    /// ��������� �����
    /// </summary>
    private void WaveGeneration()
    {

    }


    private void StartWave()
    {

    }
    


}
