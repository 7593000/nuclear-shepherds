using System;
using UnityEngine;

[CreateAssetMenu( fileName = "_friend" , menuName = "Configuration/add config units" , order = 1 )]

public class UnitConfig : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField, Tooltip( "Бро или нет" )]
    private TypeUnit _unitType;
    private LevelEnemy _enemy;
    [Space]
    [SerializeField, Tooltip( "Имя юнита" )]
    private string _name;

    [SerializeField, Tooltip( "Количество жизней" )]
    private float _health;

    [SerializeField, Tooltip( "Шанс поразить цель" ), Range( 0.1f , 1f )]
    private float _luck;
 
    [SerializeField, Tooltip( "Занимаемое количество клеток на карте при установки юнита" ), Range( 1 , 19 )]
    private int _occupiedArea;

    [SerializeField, Tooltip( "Сокорсть движения" )]
    private float _speed;

 

    [SerializeField, Tooltip( "Тип используемого оружия" )]
    private TypeWeapons _typeWeapon;
    [SerializeField, Tooltip( "Конфиг для оружия" )]
    private WeaponsConfig _config;

    [Space]
    [Header("Настройки для обновления юнита")]
    [SerializeField, Tooltip( "Стоимость найма : количество едениц получаемых за убийсмтво" )]
    private int _cost;
    [SerializeField, Tooltip( "Стоиомсть повышения юнита" )]
    private int _costUpgrade;
    [Header( "Коэффициенты повышения характеристик:" )]
    [SerializeField, Tooltip( "Коэффициент урона" )]
    public float _damageRatio;
    [SerializeField, Tooltip( "Коэффициент скорости атаки" )]
    public float _speedAttackRatio;
    [SerializeField, Tooltip( "Коэффициент удачи" )]
    public float _luckRatio;

    [Space]
    [Header( "Характеристики защиты" )]
    [SerializeField, Tooltip( "Защита от огнестрельного оружия" )] private float _firearms;
    [SerializeField, Tooltip( "Защита от огня" )] private float _fire;
    [SerializeField, Tooltip( "Защита от энергетического оружия" )] private float _energyWeapons;
    [SerializeField, Tooltip( "защита от взрыва" )] private float _explosionProtection;
    [ Space]
    [SerializeField, Tooltip( "Префаб юнита" )] private UnitComponent _unitPrefab;

    [Space]
    [SerializeField, Tooltip( "Спрайт юнита" )]
    private Sprite _sprite;

    public int GetId => _id;


    /// <summary>
    /// Получить принадлежность юнита 
    /// </summary>

    public TypeUnit GetTypeUnit => _unitType;

    /// <summary>
    /// Получить имя юнита
    /// </summary>
    public string GetName => _name;

    /// <summary>
    /// Стоимость юнита
    /// </summary>
    public int GetCost => _cost;
    /// <summary>
    /// Получить данные для обнавления юнита:[стоимость обнавления, урон, скорость атаки, удача] 
    /// </summary>
    public float[] GetRatio => new float[] {_costUpgrade, _damageRatio , _speedAttackRatio , _luckRatio };
 

    /// <summary>
    /// Количество жизней
    /// </summary>
    public float GetHealth => _health;

    /// <summary>
    /// Получмить уровень удачи при попадании
    /// </summary>
    public float GetLuck => _luck;

    /// <summary>
    /// Получить дистанцию атаки
    /// </summary>
   // public float GetDistance => _distance;

    /// <summary>
    /// Получить тип используемого оружия
    /// </summary>
    public TypeWeapons GetTypeWeapons => _typeWeapon;

    /// <summary>
    /// Получить количество занимаемых клеток на поле
    /// </summary>
    public int GetOccupiedArea => _occupiedArea;

    /// <summary>
    /// Получить скорость передвижения юнита
    /// </summary>
    public float GetSpeed => _speed;

    /// <summary>
    /// Получить коэффициент защиты от огнестрельного оружия
    /// </summary>
    public float GetProtectionFirearms => _firearms;

    /// <summary>
    /// Получить коэффициент защиты от огня
    /// </summary>
    public float GetProtectionFire => _fire;

    /// <summary>
    /// Получить коэффициент защиты от энергетического оружия
    /// </summary>
    public float GetProtectionEnergyWeapons => _energyWeapons;
    /// <summary>
    ///  Получить коэффициент защиты от взрыва: Ракетницы, снаряды .. ." 
    /// </summary>
    public float GetProtectionExplosion => _explosionProtection;
    /// <summary>
    /// Получить конфиг используемого оружия
    /// </summary>
    public WeaponsConfig GetWeaponsConfig => _config;

    /// <summary>
    /// Получить префаб юнита
    /// </summary>
    public UnitComponent GetPrefab => _unitPrefab;

    /// <summary>
    /// Получить спрайт юнита
    /// </summary>
    public Sprite GetSprite => _sprite;


 
    
}
