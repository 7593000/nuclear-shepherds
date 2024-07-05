
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
/// ��� ������
/// </summary>
public enum TypeWeapons  : byte
{

    NONE,
    /// <summary>
    /// ��� ������,���������� ���
    /// </summary>
    NOWEAPON,
    /// <summary>
    /// ��������
    /// </summary>
    GUN,
    /// <summary>
    /// �������
    /// </summary>
    SLEDGEHAMMER,
    /// <summary>
    /// ������� 
    /// </summary>
    FLAMETHROWER,
    /// <summary>
    /// ���������
    /// </summary>
    ROCKETLAUNCHER,
    /// <summary>
    /// �����
    /// </summary>
    LASER,
    /// <summary>
    /// �������
    /// </summary>
    MINIGUN,
    /// <summary>
    /// ������������� ������
    /// </summary>
    ELECTRICCHARGES,
    /// <summary>
    /// �������
    /// </summary>
    SHELLS

}

public enum StatusWeapon : byte
{
    NONE,
    /// <summary>
    /// ������� ����
    /// </summary>
    DAMAGE,
    /// <summary>
    /// �����������
    /// </summary>
    RECHARGE 
   
}


/// <summary>
/// ��������� ����� 
/// </summary>
public enum StateUnit : byte
{
    NONE,
    /// <summary>
    /// �����������
    /// </summary>
    IDLE,
    /// <summary>
    /// ��������
    /// </summary>
    MOVE,
    /// <summary>
    /// �����
    /// </summary>
    ATTACK,
    /// <summary>
    /// ����� ������� ��� �����
    /// </summary>
    SEARCH,
    /// <summary>
    /// ���� �����
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
/// ������������ ������ ������ ��� �������� ������
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