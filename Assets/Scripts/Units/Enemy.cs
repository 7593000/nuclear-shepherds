using UnityEngine;

public class Enemy : UnitComponent, IHealth, IAttack, IMovable

{
    public bool IsDead { get; set; } = false;
   
    public void Move()
    {

        float step = _config.GetSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards( transform.position , GetTarget.position , step );

    }

    public void TakeDamage( float damage )
    {
        

        if ( !IsDead )
        {
            _health.TakeDamage( damage );
        }
        else
        {
            gameObject.SetActive( false );  
        }


    }
     
    public float Health() => _config.GetHealth;

    public void Attack()
    {

        GetAttack.AttackTarget( GetTargetForAttack );


        //SetState(NoneState);
        //gameObject.SetActive(false);


    }


    protected override void Initialized()
    {
        base.Initialized();
        _health.Container( this );
    }
}

