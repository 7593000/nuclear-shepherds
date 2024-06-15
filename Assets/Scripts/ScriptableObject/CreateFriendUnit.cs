using UnityEngine;

[CreateAssetMenu( fileName = "_unit" , menuName = "Configuration/Units/Friends" , order = 1 )]

public class FriendUnit : ScriptableObject
{
    /// <summary>
    /// Имя юнита
    /// </summary>
    [SerializeField] private string _name;
    /// <summary>
    ///Шанс поразить цель
    /// </summary>
    [SerializeField, Range( 0.1f , 1f )]
    private float _luck;
    /// <summary>
    ///Дальность атаки
    /// </summary>
    [SerializeField, Tooltip( "Дальность атаки" ), Range( 0f , 100f )]
    private float _distance;
    /// <summary>
    /// Тип используемого оржуия
    /// </summary>
    [SerializeField, Tooltip( "Тип используемого оружия" )]
    private TypeWeapons _typeWeapon;
    /// <summary>
    /// Занимаемое количество клеток на карте при установки юнита
    /// </summary>
    [SerializeField, Tooltip( "Занимаемое количество клеток на карте при установки юнита" ), Range( 1 , 19 )]
    private int _occupiedArea;

    [SerializeField, Tooltip( "Стоимость найма" )]
    private int _cost;

    [SerializeField, Tooltip( "Спрайт оружия для визуализации в инспекторе" )]
    private Sprite _sprite;
    /// <summary>
    /// Получить имя юнита
    /// </summary>
    public string GetName => _name;
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
    public int GetOCcupiedArea => _occupiedArea;
}
