using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// Расчет нанесения урона оружием 
/// </summary>
public class Damage
{
    WeaponsConfig _weapon;

    private bool _canAttack = true;
    public float _speedAttack;
    private float _duration;
    private float _rechargeTime;
    public float DamageValue { get; protected set; }
    public float Luck { get; protected set; }

    public Damage( WeaponsConfig weapon , float luck )
    {
        _weapon = weapon;
        _duration = _weapon.GetDuratuion;
        _rechargeTime = _weapon.GetRechargeTime; 
        _speedAttack = _weapon.GetSpeedAttack;
        DamageValue = _weapon.GetDamage;
        Luck = luck;
    }

    private async Task AttackCooldown( float attackSpeed )
    {
        await Task.Delay( ( int )( attackSpeed * 1000 ) );
    }

    public float DamageTarget()
    {
        //TODO => вставить отслеждивание Duration оружия .
        if ( _canAttack )
        {
            _canAttack = false;

            float damage = CalculatingDamage();
            CooldownWeapon(); // Запуск перезарядки

            return damage;
        }
        return -1;
    }

    private async void CooldownWeapon()
    {
        await AttackCooldown( _speedAttack );
        _canAttack = true;
    }

    /// <summary>
    /// Рассчет наносимого урона: урон * удача
    /// </summary>
    /// <returns></returns>
    private float CalculatingDamage()
    {
        if ( CritCalculation() )
        {
            return DamageValue + ( DamageValue * Luck );
        }
        return DamageValue;
    }

    /// <summary>
    /// Расчет крита 
    /// </summary>
    /// <param name="luck"></param>
    /// <returns></returns>
    private bool CritCalculation() => RandomRange() < Luck;

    public float RandomRange()
    {
        return Random.Range( 0f , 1f );
    }
 
 
}
