 
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ������ ��������� ����� ������� 
/// </summary>
[System.Serializable]
public class Damage
{
    private WeaponsConfig _weapon;
    [SerializeField] private bool _canAttack = true;
    [SerializeField] private bool _isRecharge = false;
    [SerializeField] private float _speedAttack;
    [SerializeField] private int _rechargeTime;
    [SerializeField] private int _weaponAmmo;
    [SerializeField] private float _luck;
    private float _damage;

    public Damage(WeaponsConfig weapon, float luck)
    {
        _weapon = weapon;
        _speedAttack = _weapon.GetSpeedAttack;
        _rechargeTime = _weapon.GetRechargeTime;
        _weaponAmmo = _weapon.GetWeaponAmmo;
        _luck = luck;
        _damage = _weapon.GetDamage;
    }

    /// <summary>
    /// ��������� �������� �����
    /// </summary>
    /// <param name="newLuck"></param>
    public void SetLuck(float newLuck)
    {
        _luck = newLuck;
    }

    /// <summary>
    /// ��������� �������� �����
    /// </summary>
    /// <param name="newSpeedAttack"></param>
    public void SetSpeedAttack(float newSpeedAttack)
    {
        _speedAttack = newSpeedAttack;
    }

    /// <summary>
    /// ��������� �����
    /// </summary>
    /// <param name="newDamage"></param>
    public void SetDamage(float newDamage)
    {
        _damage = newDamage;
    }

    /// <summary>
    /// ��������� ����� ����
    /// </summary>
    /// <returns>�������� ����� ��� -1, ���� ����� ����������</returns>
    public float DamageTarget()
    {
        if (!WeaponAmmoCount())
        {
            Debug.Log("�����������!");
            // ������ �����������
            RechargeWeapon();

            return -100;


        }


        if (_canAttack && !_isRecharge )
        {
            float damage = CalculatingDamage();
            _weaponAmmo--;
           
            if(_speedAttack  > 0f)
            {
                CooldownWeapon();
            }
         
            Debug.Log("�������!");
            return damage;
        }
       

        return -1;
    }

    /// <summary>
    /// �������� ������� �����������
    /// </summary>
    /// <returns>true, ���� ���������� ����, ����� false</returns>
    private bool WeaponAmmoCount()
    {
        return _weaponAmmo > 0;
    }

    /// <summary>
    /// ������� ���������� �����: ���� * �����
    /// </summary>
    /// <returns>�������� �����</returns>
    private float CalculatingDamage()
    {
        if (CritCalculation())
        {
            return _damage + (_damage * _luck);
        }
        return _damage;
    }

    /// <summary>
    /// ������ �����
    /// </summary>
    /// <returns>true, ���� ����������� ���� ���������, ����� false</returns>
    private bool CritCalculation() => RandomRange() < _luck;

    /// <summary>
    /// ��������� ���������� �����
    /// </summary>
    /// <returns>��������� ����� �� 0 �� 1</returns>
    public float RandomRange()
    {
        return Random.Range(0f, 1f);
    }

    #region ASYNC METHOD
    // TODO=> ������� � ���� �����
    /// <summary>
    /// ������ ������  �������� �����
    /// </summary>
    /// <param name="typeCoolDown">����� ��������</param>
    private async void CooldownWeapon( )
    {
        _canAttack = false;
        await AttackCooldown(_speedAttack);
        _canAttack = true;
    }

    /// <summary>
    /// ������ ������ �����������
    /// </summary> 
    private async void RechargeWeapon()
    {
        _isRecharge = true; //todo -> _isRecharge �� ��������, �����
        _canAttack = false;
        await AttackCooldown(_rechargeTime);
        _isRecharge = false;
        _canAttack = true;
     
    _weaponAmmo = _weapon.GetWeaponAmmo;
    }

    /// <summary>
    /// �������� �����
    /// </summary>
    /// <param name="timer">����� �������� � ��������</param>
    /// <returns>Task ��� ������������ ��������</returns>
    private async Task AttackCooldown(float timer)
    {
        await Task.Delay((int)(timer * 1000));
    }

    #endregion
}
