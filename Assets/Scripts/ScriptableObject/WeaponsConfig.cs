using UnityEngine;
[CreateAssetMenu( fileName = "_weapon" , menuName = "Configuration/Weapons" , order = 1 )]
public class WeaponsConfig : ScriptableObject
{
    [SerializeField, Tooltip( "Тип оружия" )]
    private TypeWeapons _type;
   
    //TODO => DEL
    [SerializeField, Tooltip("Продолжительность воздействия оружия")]
    private float _duration;
   
    [SerializeField, Tooltip( "Скорость атаки" )]
    private float _speedAttack;
    [SerializeField, Tooltip("Время перезарядки/Время новой атаки")]
    private float _rechargeTime;

    [SerializeField, Tooltip( "Наносимый урод" )]
    private float _damage;
    [Space]
    [SerializeField,Tooltip("Звук выстрела")]
    private AudioClip _audioClip;

#if UNITY_EDITOR
    [SerializeField, Tooltip( "Спрайт оружия для визуализации в инспекторе" )]
    private Sprite _sprite;
#endif
    /// <summary>
    /// Продолжительность воздействия оружия
    /// </summary>
    public float GetDuratuion => _duration;
    /// <summary>
    /// Скорость атаки
    /// </summary>
    public float GetSpeedAttack => _speedAttack;
    /// <summary>
    /// Время перезарядки
    /// </summary>
    public float GetRechargeTime => _rechargeTime;
    public float GetDamage => _damage;

    public AudioClip GetAudioClip => _audioClip;

#if UNITY_EDITOR
    public Sprite GetSprite => _sprite;
#endif
}
