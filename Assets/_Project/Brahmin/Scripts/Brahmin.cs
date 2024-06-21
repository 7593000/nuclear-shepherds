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
                gameObject.SetActive( false );
            }
        }
        else
        {
            gameObject.SetActive( false );
        }


    }


    protected override void Initialized()
    {
        base.Initialized();
        _health.Container( this );
    }
}
