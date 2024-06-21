using UnityEngine;

public class Damage
{
    WeaponsConfig _weapon;

    private float _speedAttackTemp = 0;
    private float _rechargeTimeTemp = 0;
    public float SpeedAttack { get; protected set; }
    public float DamageValue { get; protected set; }
    public float Luck { get; protected set; }


    public Damage(WeaponsConfig weapon, float luck)
    {
        _weapon = weapon;
        DamageValue = _weapon.GetDamage;
        SpeedAttack = _weapon.GetSpeedAttack;
        Luck = luck;
    }

    public void DamageTarget( IHealth unit )
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
        if (CritCalculation()) return (DamageValue + DamageValue * Luck);

        return DamageValue;
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
