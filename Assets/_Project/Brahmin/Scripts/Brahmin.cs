using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brahmin : UnitComponent, ITakeDamage, IHealth, IMovable
{
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
        _health.TakeDamage(damage);
        
    }

 
    protected override void Initialized()
    {
        base.Initialized();
        _health.Container(this);
    }
}
