/// <summary>
/// Тип оружия
/// </summary>
public enum TypeWeapons  : byte
{

    NONE,
    /// <summary>
    /// Без оружия,рукопашный бой
    /// </summary>
    NOWEAPON,
    /// <summary>
    /// Пистолет
    /// </summary>
    GUN,
    /// <summary>
    /// Кувалда
    /// </summary>
    SLEDGEHAMMER,
    /// <summary>
    /// Огнемет 
    /// </summary>
    FLAMETHROWER,
    /// <summary>
    /// Ракетница
    /// </summary>
    ROCKETLAUNCHER,
    /// <summary>
    /// Лазер
    /// </summary>
    LASER,
    /// <summary>
    /// Миниган
    /// </summary>
    MINIGUN,
    /// <summary>
    /// Электрические заряды
    /// </summary>
    ELECTRICCHARGES,
    /// <summary>
    /// Снаряды
    /// </summary>
    SHELLS

}

/// <summary>
/// Состояние юнита 
/// </summary>
public enum StateUnit : byte
{
    NONE,
    /// <summary>
    /// Бездействие
    /// </summary>
    IDLE,
    /// <summary>
    /// Движение
    /// </summary>
    MOVE,
    /// <summary>
    /// Атака
    /// </summary>
    ATTACK,
    /// <summary>
    /// Поиск брамина для атаки
    /// </summary>
    SEARCH,
    /// <summary>
    /// Юнит мертв
    /// </summary>
    DEAD
}

public enum StateUnitList : byte
{
    MOVE,
    OTHER,
    ATTACK
}