public class Brahmin : UnitComponent, IHealth, IMovable
{
    public BrahminManager _manager;
    public bool IsDead { get; set; } = false;
    public bool IsActive { get; private set; } = true;

    public void Initialized(BrahminManager manager)
    {
        _manager = manager;
    }

    public float Health()
    {
        return _config.GetHealth;
    }

    public void Move()
    {
        //TODO => Выбор рандомной точки от начальной. возврат на начальную точку. повтор.
    }

    public void TakeDamage( float damage )
    {
        if ( !IsActive )
        {
            return;
        }

        if ( !IsDead )
        {
            float health = _health.TakeDamage( damage );
            if ( health <= 0 )
            {
                DeactiveUnit();
                _manager.DeadBrahmin(this);
            }
        }
        


    }


    protected override void Initialized()
    {
        base.Initialized();
        _health.Container( this );
    }
}
