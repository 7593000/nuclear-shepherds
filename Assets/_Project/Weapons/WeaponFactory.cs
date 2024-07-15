
public static class WeaponFactory
{
    public static IAttack CreateWeapon(TypeWeapons type, UnitComponent unit)
    {

        switch (type)
        {
            case TypeWeapons.NONE:
                return null;

            case TypeWeapons.NOWEAPON:
            case TypeWeapons.SLEDGEHAMMER:
            case TypeWeapons.GUN:
                return new SightingWeapons(unit);// Класс для оружия прицельного действия: пистолет, оружие ближнего боя


            case TypeWeapons.LASER:
            case TypeWeapons.FLAMETHROWER:
            case TypeWeapons.MINIGUN: //TODO=> PiercingWeapon ? 
                return new PiercingWeapon(unit);//Класс для оружия, которе наносит урон по длине выстрела

            case TypeWeapons.SHELLS:
            case TypeWeapons.ROCKETLAUNCHER:
                return new AoEweapons(unit);//Класс для оружия АоЕ действия в точке.

            case TypeWeapons.ELECTRICCHARGES:
                return new DischargeElectricity(unit);
            default:
                return null;
        }
    }
}