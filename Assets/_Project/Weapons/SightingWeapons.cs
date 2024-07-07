/// <summary>
/// Класс для оружия прицельного действия: пистолет, оружие ближнего боя
/// </summary>
public class SightingWeapons : IAttack
{
    private UnitComponent _unit;
    private TypeWeapons _typeWeapon;
    public SightingWeapons( UnitComponent unit )
    {
        _unit = unit;
        _typeWeapon = _unit.GetConfig.GetWeaponsConfig.GetTypeWeapons;
    }

    public void Attack( float damage )
    {
        _unit.GetTargetForAttack.TakeDamage( _typeWeapon , damage );
    }
}
