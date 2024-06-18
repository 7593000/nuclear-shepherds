
using UnityEngine;


public abstract class WeaponsComponent
{
    [SerializeField]
    private WeaponsConfig _config;
    [SerializeField]
    private AudioClip _audio;
    [SerializeField]
    private Animator _animator;

    private float _speedAttackTemp = 0;
    private float _rechargeTimeTemp = 0;
    public float SpeedAttack { get; private set; }
    public float Damage { get; private set; }


    WeaponsComponent()
    {
        SpeedAttack = _config.GetSpeedAttack;
        Damage = _config.GetDamage;

    }


    //TODO => Перенести в отдельный класс , привязать к интерфейсу оружия. 
    public void Attack(UnitComponent unit)
    {


        if (!RechargeTime()) return;

        if (CalculatingAttackSpeed()) return;
        _speedAttackTemp = 0f;

        unit.GetTargetForAttack.Health(CalculatingDamage(unit.Luck));



    }

    /// <summary>
    /// Метод перезагрузки, включить анимацию состояния ожидания, прекратить атаку и начать отчет времени перезарядки 
    /// </summary>
    private bool RechargeTime()
    {
        _rechargeTimeTemp += Time.deltaTime;
        return _rechargeTimeTemp <= _config.GetRechargeTime;
      
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
    private float CalculatingDamage(float luck)
    {
        if (CritCalculation(luck)) return (Damage + Damage * luck / 100);

        return Damage;
    }

    /// <summary>
    /// Расчет крита 
    /// </summary>
    /// <param name="luck"></param>
    /// <returns></returns>
    public bool CritCalculation(float luck) => RandomRange() < luck;


    public float RandomRange()
    {
        return Random.Range(0f, 1f);
    }

}
