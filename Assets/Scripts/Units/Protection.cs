/// <summary>
/// Класс для рассчета защиты юнита от возможных вариантов атаки 
/// </summary>
public class Protection
{
    private UnitComponent _unit;
    private float _firearms;
    private float _energyWeapons;
    private float _fire;
    private float _explosionProtection;
    public Protection( UnitComponent unit )
    {
        _unit = unit;
        _firearms = unit.GetConfig.GetProtectionFirearms;
        _energyWeapons = unit.GetConfig.GetProtectionEnergyWeapons;
        _fire = unit.GetConfig.GetProtectionFire;
        _explosionProtection = unit.GetConfig.GetProtectionExplosion;
    }

    public float CalculationProtection( TypeWeapons type )
    {
        float protect = 0;

        switch ( type )
        {
            case TypeWeapons.NONE:
                protect = 0;
                break;

            case TypeWeapons.SLEDGEHAMMER:
            case TypeWeapons.NOWEAPON:
                protect = 0;
                break;

            case TypeWeapons.GUN:
            case TypeWeapons.MINIGUN:
                protect = _firearms;
                break;

            case TypeWeapons.FLAMETHROWER:
                protect = 0;
                break;

            case TypeWeapons.ROCKETLAUNCHER:
            case TypeWeapons.SHELLS:
                protect = _explosionProtection;
                break;

            case TypeWeapons.ELECTRICCHARGES:
            case TypeWeapons.LASER:
                protect = 0;
                break;
 

        }
        return protect;
    }
}