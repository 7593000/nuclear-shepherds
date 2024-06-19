public class Weapon : WeaponsComponent
//TODO=> DEL

{
    public Weapon(WeaponsConfig config)
    {
        _config = config;
        Damage = _config.GetDamage;
        SpeedAttack = _config.GetSpeedAttack;   
    }

   
   
}
