using System.Collections.Generic;
using UnityEngine;
 /// <summary>
 /// Класс для оружия, которе наносит урон по длине выстрела: Лазер. огнемет, электро 
 /// </summary>
[System.Serializable]
public class PiercingWeapon : IAttack
{
    private UnitComponent _unit;
    private TypeWeapons _typeWeapon;
    private List<IHealth> _targetsAttack = new();
    private float _radius =1f;
    private int _maxTargets = 10; // Максимальное количество целей
    private RaycastHit2D[] _hits;
    public PiercingWeapon( UnitComponent unit )
    {
        _unit = unit;
        _radius = unit.GetConfig.GetWeaponsConfig.GetRadiusAoE;
        _maxTargets = unit.GetConfig.GetWeaponsConfig.GetNumberStriking;
  _typeWeapon = _unit.GetConfig.GetWeaponsConfig.GetTypeWeapons;
  
        _hits = new RaycastHit2D[ _maxTargets ];
    }

    public void Attack( float damage )
    {
        DetectTargets();
       
        foreach ( IHealth target in _targetsAttack )
        {
            target.TakeDamage( _typeWeapon , damage );
        }

        //targer.TakeDamage( damage );

    }


    /// <summary>
    /// Наносимый урон по длине 
    /// </summary>
    private void DetectTargets()
    {
        _targetsAttack.Clear();

        Transform startPoint = _unit.transform;
        Transform target = _unit.GetTarget ;

        Vector2 direction = ( target.position - startPoint.position ).normalized;
        float distance = Vector2.Distance( startPoint.position , target.position );

#if UNITY_EDITOR
        Debug.DrawLine( startPoint.position , target.position , Color.red , 2f );
#endif
        int hitCount = Physics2D.CircleCastNonAlloc( startPoint.position , _radius , direction , _hits , distance );

        
        for ( int i = 0; i < hitCount; i++ )
        {
            Debug.Log( $"Hit: {_hits[ i ].collider.name}" );
            if ( _hits[ i ].collider.TryGetComponent<IHealth>( out IHealth unit ) )
            {
                _targetsAttack.Add( unit );
            }
        }
    }





}