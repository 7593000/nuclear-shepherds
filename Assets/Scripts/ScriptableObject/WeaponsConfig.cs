using UnityEngine;
[CreateAssetMenu( fileName = "_weapon" , menuName = "Configuration/Weapons" , order = 2 )]
public class WeaponsConfig : ScriptableObject
{
    [SerializeField, Tooltip( "Тип оружия" )]
    private TypeWeapons _type;
    //[SerializeField, Tooltip( "Воздействие по площади" )] private bool _areaOfEffect;
    [SerializeField, Tooltip( "Количество юнитов попадающие под воздействие оружия AoE" )] private int _numberStriking = 1;
    [SerializeField, Tooltip( "Радиус воздейсвия оружия  ( для АоЕ ) " )]
    private float _radiusAoE = 0f;
   
    [SerializeField, Tooltip( "Скорость атаки" )]
    private float _speedAttack;
    [SerializeField, Tooltip("Время перезарядки/Время новой атаки")]
    private int _rechargeTime;

    [SerializeField,Tooltip("Количество боеприпасов у оружия")] private int _weaponAmmo;
    [SerializeField, Tooltip( "Наносимый урод" )]
    private float _damage;
    [SerializeField, Tooltip( "Дистанция атаки" )]
    private float _distance;
    [Space]
    [SerializeField,Tooltip("Звук выстрела")]
    private AudioClip[] _audioClip;

#if UNITY_EDITOR
    [SerializeField, Tooltip( "Спрайт оружия для визуализации в инспекторе" )]
    private Sprite _sprite;
#endif

    public TypeWeapons GetTypeWeapons => _type;
    /// <summary>
    /// Получить количество юнитов попадающие под воздействие оружия
    /// </summary>
    public int GetNumberStriking => _numberStriking;
    public float GetRadiusAoE => _radiusAoE;
    /// <summary>
    /// Скорость атаки
    /// </summary>
    public float GetSpeedAttack => _speedAttack;
    /// <summary>
    /// Время перезарядки
    /// </summary>
    public int GetRechargeTime => _rechargeTime;
    /// <summary>
    /// Получить количество боеприпасов у оружия
    /// </summary>
    public int GetWeaponAmmo => _weaponAmmo;    
    /// <summary>
    /// Урон оружия
    /// </summary>
    public float GetDamage => _damage;
    /// <summary>
    /// Дистанция атаки
    /// </summary>
    public float GetDistance => _distance;
    /// <summary>
    /// Получить звук использования оружием
    /// </summary>
    public AudioClip[] GetAudioClip => _audioClip;
       
#if UNITY_EDITOR
    public Sprite GetSprite => _sprite;
#endif
}
