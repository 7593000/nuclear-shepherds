using UnityEngine;

public class Enemy : UnitComponent, ITakeDamage, IHealth, IAttack, IMovable

{


    public void Move()
    {

        var step = _config.GetSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GetTarget.position, step);

    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }
    public float Health() => _config.GetHealth;

    public void Attack()
    {
        GetWeapons.Attack(this);  
    }

  
    protected override void Initialized()
    {
        base.Initialized();
        _health.Container(this);
    }
}
   
