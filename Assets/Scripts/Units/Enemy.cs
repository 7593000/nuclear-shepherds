using System;
using UnityEngine;

public class Enemy : UnitComponent, IHealth, IAttack, IMovable

{

    public static event Action<int> OnCoins;


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
            float health = _health.TakeDamage( damage );
          
            if ( health <= 0 )
            {

                gameObject.SetActive( false );
                Debug.Log("Противник уничтожен");

                OnCoins?.Invoke(GetCost);
               
            }
        }
        else //TODO => DEL? 
        {
            gameObject.SetActive( false );
        }


    }

    public float Health() => _config.GetHealth;

    public void Attack()
    {

        float damage = GetDamane.DamageTarget();
       

        if ( damage >= 0 )
        {
            StartAnimation.ToRun( StateUnit.ATTACK );
            GetTargetForAttack.TakeDamage( damage );

        }

    }

    protected override void Initialized()
    {
        base.Initialized();
        _health.Container( this );
    }

    protected override void Start()
    {
        base.Start();
        SetState( MoveState );
    }

}

