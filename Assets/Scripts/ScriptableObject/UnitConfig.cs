using System;
using UnityEngine;

[CreateAssetMenu( fileName = "_friend" , menuName = "Configuration/add config units" , order = 1 )]

public class UnitConfig : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField, Tooltip( "��� ��� ���" )]
    private TypeUnit _unitType;
    private LevelEnemy _enemy;
    [Space]
    [SerializeField, Tooltip( "��� �����" )]
    private string _name;

    [SerializeField, Tooltip( "���������� ������" )]
    private float _health;

    [SerializeField, Tooltip( "���� �������� ����" ), Range( 0.1f , 1f )]
    private float _luck;
 
    [SerializeField, Tooltip( "���������� ���������� ������ �� ����� ��� ��������� �����" ), Range( 1 , 19 )]
    private int _occupiedArea;

    [SerializeField, Tooltip( "�������� ��������" )]
    private float _speed;

 

    [SerializeField, Tooltip( "��� ������������� ������" )]
    private TypeWeapons _typeWeapon;
    [SerializeField, Tooltip( "������ ��� ������" )]
    private WeaponsConfig _config;

    [Space]
    [Header("��������� ��� ���������� �����")]
    [SerializeField, Tooltip( "��������� ����� : ���������� ������ ���������� �� ���������" )]
    private int _cost;
    [SerializeField, Tooltip( "��������� ��������� �����" )]
    private int _costUpgrade;
    [Header( "������������ ��������� �������������:" )]
    [SerializeField, Tooltip( "����������� �����" )]
    public float _damageRatio;
    [SerializeField, Tooltip( "����������� �������� �����" )]
    public float _speedAttackRatio;
    [SerializeField, Tooltip( "����������� �����" )]
    public float _luckRatio;

    [Space]
    [Header( "�������������� ������" )]
    [SerializeField, Tooltip( "������ �� �������������� ������" )] private float _firearms;
    [SerializeField, Tooltip( "������ �� ����" )] private float _fire;
    [SerializeField, Tooltip( "������ �� ��������������� ������" )] private float _energyWeapons;
    [SerializeField, Tooltip( "������ �� ������" )] private float _explosionProtection;
    [ Space]
    [SerializeField, Tooltip( "������ �����" )] private UnitComponent _unitPrefab;

    [Space]
    [SerializeField, Tooltip( "������ �����" )]
    private Sprite _sprite;

    public int GetId => _id;


    /// <summary>
    /// �������� �������������� ����� 
    /// </summary>

    public TypeUnit GetTypeUnit => _unitType;

    /// <summary>
    /// �������� ��� �����
    /// </summary>
    public string GetName => _name;

    /// <summary>
    /// ��������� �����
    /// </summary>
    public int GetCost => _cost;
    /// <summary>
    /// �������� ������ ��� ���������� �����:[��������� ����������, ����, �������� �����, �����] 
    /// </summary>
    public float[] GetRatio => new float[] {_costUpgrade, _damageRatio , _speedAttackRatio , _luckRatio };
 

    /// <summary>
    /// ���������� ������
    /// </summary>
    public float GetHealth => _health;

    /// <summary>
    /// ��������� ������� ����� ��� ���������
    /// </summary>
    public float GetLuck => _luck;

    /// <summary>
    /// �������� ��������� �����
    /// </summary>
   // public float GetDistance => _distance;

    /// <summary>
    /// �������� ��� ������������� ������
    /// </summary>
    public TypeWeapons GetTypeWeapons => _typeWeapon;

    /// <summary>
    /// �������� ���������� ���������� ������ �� ����
    /// </summary>
    public int GetOccupiedArea => _occupiedArea;

    /// <summary>
    /// �������� �������� ������������ �����
    /// </summary>
    public float GetSpeed => _speed;

    /// <summary>
    /// �������� ����������� ������ �� �������������� ������
    /// </summary>
    public float GetProtectionFirearms => _firearms;

    /// <summary>
    /// �������� ����������� ������ �� ����
    /// </summary>
    public float GetProtectionFire => _fire;

    /// <summary>
    /// �������� ����������� ������ �� ��������������� ������
    /// </summary>
    public float GetProtectionEnergyWeapons => _energyWeapons;
    /// <summary>
    ///  �������� ����������� ������ �� ������: ���������, ������� .. ." 
    /// </summary>
    public float GetProtectionExplosion => _explosionProtection;
    /// <summary>
    /// �������� ������ ������������� ������
    /// </summary>
    public WeaponsConfig GetWeaponsConfig => _config;

    /// <summary>
    /// �������� ������ �����
    /// </summary>
    public UnitComponent GetPrefab => _unitPrefab;

    /// <summary>
    /// �������� ������ �����
    /// </summary>
    public Sprite GetSprite => _sprite;


 
    
}
