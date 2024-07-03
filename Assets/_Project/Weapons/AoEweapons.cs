/// <summary>
///  ласс оружи€ наносимый јо≈ урон в точке: ракетницы 
/// </summary>
public class AoEweapons : IAttack
{
    private UnitComponent _unit;

    public AoEweapons( UnitComponent unit )
    {

        _unit = unit;
    }

    public void Attack( float damage )
    {

    }
}
