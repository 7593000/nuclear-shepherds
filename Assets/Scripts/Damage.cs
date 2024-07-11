using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Расчет нанесения урона оружием 
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

    private Task _cooldownTask;
    private Task _rechargeTask;

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
    /// Изменения значения удачи
    /// </summary>
    /// <param name="newLuck"></param>
    public void SetLuck(float newLuck)
    {
        _luck = newLuck;
    }

    /// <summary>
    /// Изменения скорости атаки
    /// </summary>
    /// <param name="newSpeedAttack"></param>
    public void SetSpeedAttack(float newSpeedAttack)
    {
        _speedAttack = newSpeedAttack;
    }

    /// <summary>
    /// Изменения урона
    /// </summary>
    /// <param name="newDamage"></param>
    public void SetDamage(float newDamage)
    {
        _damage = newDamage;
    }

    /// <summary>
    /// Нанесение урона цели
    /// </summary>
    /// <returns>Значение урона или -1, если атака невозможна</returns>
    public float DamageTarget()
    {
        if (_isRecharge || !WeaponAmmoCount())
        {
            if (!_isRecharge)
            {
                // Запуск перезарядки
                _rechargeTask = RechargeWeapon();
            }
            return -100;
        }

        if (_canAttack)
        {
            float damage = _damage; //todo => temp
            _weaponAmmo--;

            if (_speedAttack > 0f)
            {
                _cooldownTask = CooldownWeapon();
            }

            return damage;
        }

        return -1;
    }

    /// <summary>
    /// Проверка наличия боеприпасов
    /// </summary>
    /// <returns>true, если боеприпасы есть, иначе false</returns>
    private bool WeaponAmmoCount()
    {
        return _weaponAmmo > 0;
    }

    #region ASYNC METHOD

    /// <summary>
    /// Запуск отчета скорость атаки
    /// </summary>
    /// <param name="typeCoolDown">Время задержки</param>
    private async Task CooldownWeapon()
    {
        _canAttack = false;
        await Task.Delay((int)(_speedAttack * 1000));
        _canAttack = true;
    }

    /// <summary>
    /// Запуск отчета перезарядки
    /// </summary> 
    private async Task RechargeWeapon()
    {
        _isRecharge = true;
        _canAttack = false;
        await Task.Delay((int)(_rechargeTime * 1000));
        _isRecharge = false;
        _canAttack = true;
        _weaponAmmo = _weapon.GetWeaponAmmo;
    }

    #endregion
}
