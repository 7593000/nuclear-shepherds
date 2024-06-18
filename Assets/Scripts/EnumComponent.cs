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
    ATTACK
}