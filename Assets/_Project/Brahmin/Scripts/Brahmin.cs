using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brahmin : UnitComponent, IHealth, IMovable
{

 
    public bool IsDead { get; set; } = false;
    public bool IsActive { get; private set; } = true;
   
    public float Health()
    {
     return _config.GetHealth;
    }

    public void Move()
    {
        //TODO => Выбор рандомной точки от начальной. возврат на начальную точку. повтор.
    }

    public void TakeDamage(float damage)
    {
        if ( !IsActive ) return;
        
        if ( !IsDead )
        {
            _health.TakeDamage( damage );
        }
        else
        {
            IsActive = false;
            GetAminator.SetTrigger( "Dead");
          //  gameObject.SetActive( false );
        }

    }


    protected override void Initialized()
    {
        base.Initialized();
        _health.Container(this);
    }
}
