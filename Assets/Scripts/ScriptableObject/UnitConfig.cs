using UnityEngine;

[CreateAssetMenu( fileName = "_friend" , menuName = "Configuration/add config units" , order = 1 )]

public class UnitConfig : ScriptableObject
{

    [SerializeField, Tooltip( "Бро или нет" )]
    private TypeUnit _unitType;
    [Space]
    [SerializeField, Tooltip( "Имя юнита" )]
    private string _name;

    [SerializeField, Tooltip( "Количество жизней" )]
    private float _health;

    [SerializeField, Tooltip( "Шанс поразить цель" ), Range( 0.1f , 1f )]
    private float _luck;

    [SerializeField, Tooltip( "Дальность атаки" ), Range( 0f , 100f )]
    private float _distance;

    [SerializeField, Tooltip( "Занимаемое количество клеток на карте при установки юнита" ), Range( 1 , 19 )]
    private int _occupiedArea;

    [SerializeField, Tooltip( "Сокорсть движения" )]
    private float _speed;

    [SerializeField, Tooltip( "Стоимость найма : количество едениц получаемых за убийсмтво" )]
    private int _cost;


    [SerializeField, Tooltip( "Тип используемого оружия" )]
    private TypeWeapons _typeWeapon;
    [SerializeField, Tooltip( "Конфиг для оружия" )]
    private WeaponsConfig _config;


    [Space]
    [Header( "Характеристики защиты" )]
    [SerializeField, Tooltip( "Защита от огнестрельного оружия" )] private float _firearms;
    [SerializeField, Tooltip( "Защита от огня" )] private float _fire;
    [SerializeField, Tooltip( "Защита от энергетического оружия" )] private float _energyWeapons;


#if UNITY_EDITOR
    [Space]
    [SerializeField, Tooltip( "Спрайт юнита для визуализации в инспекторе" )]
    private Sprite _sprite;
#endif
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
    public float GetDistance => _distance;
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
    /// Получить конфиг используемого оружия
    /// </summary>
    public WeaponsConfig GetWeaponsConfig => _config;
    public Sprite GetSprite => _sprite;

}
