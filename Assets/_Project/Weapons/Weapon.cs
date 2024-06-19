public class Weapon : WeaponsComponent 


{
    public Weapon(WeaponsConfig config)
    {
        _config = config;
        Damage = _config.GetDamage;
        SpeedAttack = _config.GetSpeedAttack;   
    }

   
   
}
