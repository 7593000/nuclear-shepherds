using UnityEngine;

public class Attack
{
    WeaponsConfig _weapon;

    private float _speedAttackTemp = 0;
    private float _rechargeTimeTemp = 0;
    public float SpeedAttack { get; protected set; }
    public float Damage { get; protected set; }
    public float Luck { get; protected set; }


    public Attack(WeaponsConfig weapon, float luck)
    {
        _weapon = weapon;
        Damage = _weapon.GetDamage;
        SpeedAttack = _weapon.GetSpeedAttack;
        Luck = luck;
    }

    public void AttackTarget(ITakeDamage unit)
    {
        //  if (!RechargeTime()) return;

        // if (CalculatingAttackSpeed()) return;
        _speedAttackTemp = 0f;
                
            unit.TakeDamage(CalculatingDamage());
         

    }



    /// <summary>
    /// Метод перезагрузки, включить анимацию состояния ожидания, прекратить атаку и начать отчет времени перезарядки 
    /// </summary>
    private bool RechargeTime()
    {
        _rechargeTimeTemp += Time.deltaTime;
        return _rechargeTimeTemp <= _weapon.GetRechargeTime;

    }
    /// <summary>
    /// Расчет возможности использования оружия 
    /// </summary>
    /// <returns></returns>
    private bool CalculatingAttackSpeed()
    {
        _speedAttackTemp += Time.deltaTime;
        return _speedAttackTemp >= SpeedAttack;
    }
    /// <summary>
    /// Рассчет наносимого урона: урон * удача
    /// </summary>
    /// <returns></returns>
    private float CalculatingDamage()
    {
        if (CritCalculation()) return (Damage + Damage * Luck);

        return Damage;
    }

    /// <summary>
    /// Расчет крита 
    /// </summary>
    /// <param name="luck"></param>
    /// <returns></returns>
    public bool CritCalculation() => RandomRange() < Luck;


    public float RandomRange()
    {
        return Random.Range(0f, 1f);
    }



}
