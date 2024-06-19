using UnityEngine;

public class Enemy : UnitComponent, ITakeDamage, IHealth, IAttack, IMovable

{
    public bool IsAlive { get; set; } = true;

    public void Move()
    {

        var step = _config.GetSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GetTarget.position, step);

    }

    public void TakeDamage(float damage)
    {
        if(_health.CurrentHealth <= 0 ) IsAlive = false;
        if (IsAlive)
        {
            _health.TakeDamage(damage);
        } 
       

    }



    public float Health() => _config.GetHealth;

    public void Attack()
    {
      if(GetTargetForAttack.IsAlive)
        {
            GetAttack.AttackTarget(GetTargetForAttack);
        }
      else
        {
            SetState(NoneState);
            gameObject.SetActive(false);
        }
      
    }


    protected override void Initialized()
    {
        base.Initialized();
        _health.Container(this);
    }
}

