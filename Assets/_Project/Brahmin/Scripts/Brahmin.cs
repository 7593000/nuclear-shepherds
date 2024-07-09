public class Brahmin : UnitComponent, IHealth, IMovable
{
    public BrahminManager _manager;
    public bool IsDead { get; set; } = false;
    public bool IsActive { get; private set; } = true;

 
    public void Initialized(BrahminManager manager, GameHub gameHub )
    {
        _manager = manager;
        Initialized( gameHub );


    }

    protected override void AddComponentsUnit()
    {
        base.AddComponentsUnit();
        _health.Container( this );
    }
    public float Health()
    {
        return _config.GetHealth;
    }

    public void Move()
    {
        //TODO => Выбор рандомной точки от начальной. возврат на начальную точку. повтор.
    }

    public void TakeDamage(TypeWeapons type , float damage )
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
                _manager.DeadBrahmin( this );
                DeactiveUnit();
             
            }
        }
        


    }


    //public override void Initialized(GameHub gameHub )
    //{
    //    base.Initialized(FindObjectOfType<GameHub>() ); //TODO=>DEL
    //  
    //}
}
