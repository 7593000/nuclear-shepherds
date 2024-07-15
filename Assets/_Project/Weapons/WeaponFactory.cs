
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
                return new SightingWeapons(unit);// ����� ��� ������ ����������� ��������: ��������, ������ �������� ���


            case TypeWeapons.LASER:
            case TypeWeapons.FLAMETHROWER:
            case TypeWeapons.MINIGUN: //TODO=> PiercingWeapon ? 
                return new PiercingWeapon(unit);//����� ��� ������, ������ ������� ���� �� ����� ��������

            case TypeWeapons.SHELLS:
            case TypeWeapons.ROCKETLAUNCHER:
                return new AoEweapons(unit);//����� ��� ������ ��� �������� � �����.

            case TypeWeapons.ELECTRICCHARGES:
                return new DischargeElectricity(unit);
            default:
                return null;
        }
    }
}