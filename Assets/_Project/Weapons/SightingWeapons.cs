/// <summary>
/// Класс для оружия прицельного действия: пистолет, оружие ближнего боя
/// </summary>
public class SightingWeapons : IAttack
{
    private UnitComponent _unit;
    public SightingWeapons( UnitComponent unit )
    {
        _unit = unit;
    }

    public void Attack( float damage )
    {
        _unit.GetTargetForAttack.TakeDamage( damage );
    }
}
