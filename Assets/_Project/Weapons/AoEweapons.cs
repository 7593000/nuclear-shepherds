using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ласс оружи€ наносимый јо≈ урон в точке: ракетницы 
/// </summary>
public class AoEweapons : IAttack
{
    private UnitComponent _unit;
    private PoolExplosion _poolExplosion;
    private Explosion _explosion;
    private List<IHealth> _strikingObjects = new();
    private AudioClip _explosionSFX;

    public AoEweapons(UnitComponent unit)
    {

        _unit = unit;
        _poolExplosion = _unit.GetGameHub.GetPoolExplosion;

        if (_poolExplosion == null) GameHub.Logger("Pool: " + _poolExplosion);

        _explosionSFX = _unit.GetConfig.GetWeaponsConfig.GetAudioClip[1];
    }
   
    public void Attack(float damage)
    {

       

        _explosion = _poolExplosion.GetExplosion();
        _explosion.transform.position = _unit.GetTarget.transform.position;
        _explosion.gameObject.SetActive(true);
        SoundEngine.Instance.PlaySound( _explosionSFX , SoundType.SFX );
      
        _strikingObjects = _explosion.Explode();
       

        foreach (var enemy in _strikingObjects)
        {
            enemy.TakeDamage(_unit.GetConfig.GetWeaponsConfig.GetTypeWeapons, damage);
        }
    }
}
