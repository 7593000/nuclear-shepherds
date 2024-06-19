using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brahmin : UnitComponent, ITakeDamage, IHealth, IMovable
{

    public bool IsAlive => true;

    bool ITakeDamage.IsAlive { get  ; set ; } = true;

    public float Health()
    {
     return _config.GetHealth;
    }

    public void Move()
    {
        //TODO => ����� ��������� ����� �� ���������. ������� �� ��������� �����. ������.
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
        Debug.Log(damage);
    }

 
    protected override void Initialized()
    {
        base.Initialized();
        _health.Container(this);
    }
}
