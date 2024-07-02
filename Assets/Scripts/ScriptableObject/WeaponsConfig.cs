using UnityEngine;
[CreateAssetMenu( fileName = "_weapon" , menuName = "Configuration/Weapons" , order = 2 )]
public class WeaponsConfig : ScriptableObject
{
    [SerializeField, Tooltip( "Тип оружия" )]
    private TypeWeapons _type;
   
 
   
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
    private AudioClip _audioClip;

#if UNITY_EDITOR
    [SerializeField, Tooltip( "Спрайт оружия для визуализации в инспекторе" )]
    private Sprite _sprite;
#endif
 
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
    public AudioClip GetAudioClip => _audioClip;

#if UNITY_EDITOR
    public Sprite GetSprite => _sprite;
#endif
}
