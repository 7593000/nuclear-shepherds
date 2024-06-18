
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


    //TODO => ��������� � ��������� ����� , ��������� � ���������� ������. 
    public void Attack(UnitComponent unit)
    {


        if (!RechargeTime()) return;

        if (CalculatingAttackSpeed()) return;
        _speedAttackTemp = 0f;

        unit.GetTargetForAttack.Health(CalculatingDamage(unit.Luck));



    }

    /// <summary>
    /// ����� ������������, �������� �������� ��������� ��������, ���������� ����� � ������ ����� ������� ����������� 
    /// </summary>
    private bool RechargeTime()
    {
        _rechargeTimeTemp += Time.deltaTime;
        return _rechargeTimeTemp <= _config.GetRechargeTime;
      
    }
    /// <summary>
    /// ������ ����������� ������������� ������ 
    /// </summary>
    /// <returns></returns>
    private bool CalculatingAttackSpeed()
    {
        _speedAttackTemp += Time.deltaTime;
        return _speedAttackTemp >= SpeedAttack;
    }
    /// <summary>
    /// ������� ���������� �����: ���� * �����
    /// </summary>
    /// <returns></returns>
    private float CalculatingDamage(float luck)
    {
        if (CritCalculation(luck)) return (Damage + Damage * luck / 100);

        return Damage;
    }

    /// <summary>
    /// ������ ����� 
    /// </summary>
    /// <param name="luck"></param>
    /// <returns></returns>
    public bool CritCalculation(float luck) => RandomRange() < luck;


    public float RandomRange()
    {
        return Random.Range(0f, 1f);
    }

}
