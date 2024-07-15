using UnityEngine;

public class Brahmin : UnitComponent, IHealth, IMovable
{
    public BrahminManager _manager;
    private Protection _protection;
    public bool IsDead { get; set; } = false;
    public bool IsActive { get; private set; } = true;

    //public override void Initialized( GameHub gameHub )
    //{
    //    base.Initialized( gameHub );
    //    _manager = GetGameHub.GetBrahmin;
    //}


    protected override void AddComponentsUnit()
    {
        base.AddComponentsUnit();
        _health.Container(this);
        _protection = new Protection(this);
        _manager = GetGameHub.GetBrahmin;
        SetState(IdleState);
    }
    public float Health()
    {
        return _config.GetHealth;
    }

    public void Move()
    {
        //TODO => Выбор рандомной точки от начальной. возврат на начальную точку. повтор.
    }
    public void TakeDamage(TypeWeapons type, float damage)
    {


        if (!IsDead)
        {

            float protectedDamage = damage * (_protection.CalculationProtection(type) / 100f);

            float resultDamage = Mathf.Max(0, damage - protectedDamage);

            float health = _health.TakeDamage(resultDamage);

            if (health <= 0)
            {
                _manager.DeadBrahmin(this);
                DeactiveUnit();



            }
        }



    }
    //public void TakeDamage(TypeWeapons type , float damage )
    //{
    //    if ( !IsActive )
    //    {
    //        return;
    //    }

    //    if ( !IsDead )
    //    {
    //        float health = _health.TakeDamage( damage );
    //        if ( health <= 0 )
    //        {
    //            _manager.DeadBrahmin( this );
    //            DeactiveUnit();

    //        }
    //    }



    //}


    //public override void Initialized(GameHub gameHub )
    //{
    //    base.Initialized(FindObjectOfType<GameHub>() ); //TODO=>DEL
    //  
    //}
}
