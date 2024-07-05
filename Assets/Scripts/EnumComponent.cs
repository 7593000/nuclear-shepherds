
public enum LevelEnemy : byte //TODO=> DEL
{
    LEVEL1,
    LEVEL2, 
    LEVEL3, 
    LEVEL4, 
    LEVEL5, 
    LEVEL6
}
public enum TypeUnit : byte
{
    NONE,
    BRO,
    BRAHMIN,
    ENEMY
}

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

public enum StatusWeapon : byte
{
    NONE,
    /// <summary>
    /// Наносит урон
    /// </summary>
    DAMAGE,
    /// <summary>
    /// Перезарядка
    /// </summary>
    RECHARGE 
   
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
    ATTACK,
    DIRECT
}

/// <summary>
/// Перечисление границ экрана для движения камеры
/// </summary>
public enum BorderType : byte
{
    NONE,
    LEFT,
    RIGHT,
    TOP,
    BOTTOM,
    ANGLETR,
    ANGLETL,
    ANGLEBR,
    ANGLEBL
}