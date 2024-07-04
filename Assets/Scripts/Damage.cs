 
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
        if (!WeaponAmmoCount())
        {
            Debug.Log("Перезярядка!");
            // Запуск перезарядки
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
         
            Debug.Log("Выстрел!");
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

    /// <summary>
    /// Рассчет наносимого урона: урон * удача
    /// </summary>
    /// <returns>Значение урона</returns>
    private float CalculatingDamage()
    {
        if (CritCalculation())
        {
            return _damage + (_damage * _luck);
        }
        return _damage;
    }

    /// <summary>
    /// Расчет крита
    /// </summary>
    /// <returns>true, если критический удар произошел, иначе false</returns>
    private bool CritCalculation() => RandomRange() < _luck;

    /// <summary>
    /// Генерация случайного числа
    /// </summary>
    /// <returns>Случайное число от 0 до 1</returns>
    public float RandomRange()
    {
        return Random.Range(0f, 1f);
    }

    #region ASYNC METHOD
    // TODO=> вынести в один метод
    /// <summary>
    /// Запуск отчета  скорость атаки
    /// </summary>
    /// <param name="typeCoolDown">Время задержки</param>
    private async void CooldownWeapon( )
    {
        _canAttack = false;
        await AttackCooldown(_speedAttack);
        _canAttack = true;
    }

    /// <summary>
    /// Запуск отчета перезарядки
    /// </summary> 
    private async void RechargeWeapon()
    {
        _isRecharge = true; //todo -> _isRecharge на удаление, мейби
        _canAttack = false;
        await AttackCooldown(_rechargeTime);
        _isRecharge = false;
        _canAttack = true;
     
    _weaponAmmo = _weapon.GetWeaponAmmo;
    }

    /// <summary>
    /// Задержка атаки
    /// </summary>
    /// <param name="timer">Время задержки в секундах</param>
    /// <returns>Task для асинхронного ожидания</returns>
    private async Task AttackCooldown(float timer)
    {
        await Task.Delay((int)(timer * 1000));
    }

    #endregion
}
