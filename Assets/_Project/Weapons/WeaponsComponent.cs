
using UnityEngine;

//TODO=> DEL


public abstract class WeaponsComponent
{
    protected WeaponsConfig _config;

    [SerializeField]
    private AudioClip _audio;
    [SerializeField]
    private Animator _animator;

    private float _speedAttackTemp = 0;
    private float _rechargeTimeTemp = 0;
    public float SpeedAttack { get; protected set; }
    public float Damage { get; protected set; }


    

    //TODO => Перенести в отдельный класс , привязать к интерфейсу оружия. 
   // public void Damage(UnitComponent unit)
          public void Attack(ITakeDamage unit, float luck =  0 )
    {

      //  if (!RechargeTime()) return;

       // if (CalculatingAttackSpeed()) return;
        _speedAttackTemp = 0f;
        unit.TakeDamage(CalculatingDamage(luck));
   



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
        if (CritCalculation(luck)) return (Damage + Damage * luck  );

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
