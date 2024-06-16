using UnityEngine;
[CreateAssetMenu( fileName = "_weapon" , menuName = "Configuration/Weapons" , order = 1 )]
public class CreateWeapons : ScriptableObject
{
    [SerializeField, Tooltip( "Тип оружия" )]
    private TypeWeapons _type;

    [SerializeField, Tooltip( "Скорость атаки" )]
    private float _speedAttack;

    [SerializeField, Tooltip( "Наносимый урод" )]
    private float _damage;

#if UNITY_EDITOR
    [SerializeField, Tooltip( "Спрайт оружия для визуализации в инспекторе" )]
    private Sprite _sprite;
#endif
    public float GetSpeedAttack => _speedAttack;
    public float GetDamage => _damage;

#if UNITY_EDITOR
    public Sprite GetSprite => _sprite;
#endif
}
