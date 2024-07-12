using UnityEngine;

public class DischargeElectricity : IAttack
{
    private UnitComponent _unit;
    private ParticleSystem _particleSystem;
    private float _damageRadius;
    public DischargeElectricity( UnitComponent unit )
    {
        _unit = unit;

        _particleSystem = unit.GetComponentInChildren<ParticleSystem>();

        if ( _particleSystem != null )
        {
            GameHub.Logger( "OK" );
        }

        _damageRadius = _unit.GetConfig.GetWeaponsConfig.GetRadiusAoE;


    }




    public void Attack( float damage )
    {
        Transform targetTransform = _unit.GetTarget; // Получаем трансформ цели
        if ( targetTransform != null && _particleSystem != null )
        {
            _particleSystem.Play();
            Vector3 directionToTarget = ( targetTransform.position - _unit.transform.position ).normalized;
            _particleSystem.transform.rotation = Quaternion.LookRotation( directionToTarget );

            _unit.GetTargetForAttack.TakeDamage( _unit.GetConfig.GetWeaponsConfig.GetTypeWeapons , damage );



            AoeDamage( damage );

        }






    }
    private void AoeDamage( float damage )
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll( _unit.GetTarget.position , _damageRadius );


        foreach ( Collider2D hitCollider in hitColliders )
        {
            if ( hitCollider.TryGetComponent<IHealth>( out IHealth enemyHealth ) )
            {

                enemyHealth.TakeDamage( _unit.GetConfig.GetWeaponsConfig.GetTypeWeapons , damage );
            }
        }
    }
}






