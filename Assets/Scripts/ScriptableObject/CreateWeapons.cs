using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu( fileName = "_weapon" , menuName = "Configuration/Weapons" , order = 1 )]
public class CreateWeapons : ScriptableObject
{
    /// <summary>
    /// Тип оружия
    /// </summary>
  [SerializeField, Tooltip("Тип оружия")] private TypeWeapons _type;
    /// <summary>
    /// Скорость атаки
    /// </summary>
    [SerializeField, Tooltip( "Скорость атаки" )] private float _speedAttack;
    /// <summary>
    /// наносимый урон
    /// </summary>
    [SerializeField, Tooltip( "Наносимый урод" )] private float _damage;
 
  [SerializeField, Tooltip( "Спрайт оружия для визуализации в инспекторе" )]  private Sprite _sprite;
    public float GetSpeedAttack => _speedAttack;
    public float GetDamage => _damage;
    public Sprite GetSprite => _sprite;
}
