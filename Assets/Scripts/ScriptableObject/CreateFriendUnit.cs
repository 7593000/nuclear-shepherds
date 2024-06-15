using UnityEngine;

[CreateAssetMenu( fileName = "_unit" , menuName = "Configuration/Units/Friends" , order = 1 )]

public class FriendUnit : ScriptableObject
{
    /// <summary>
    /// ��� �����
    /// </summary>
    [SerializeField] private string _name;
    /// <summary>
    ///���� �������� ����
    /// </summary>
    [SerializeField, Range( 0.1f , 1f )]
    private float _luck;
    /// <summary>
    ///��������� �����
    /// </summary>
    [SerializeField, Tooltip( "��������� �����" ), Range( 0f , 100f )]
    private float _distance;
    /// <summary>
    /// ��� ������������� ������
    /// </summary>
    [SerializeField, Tooltip( "��� ������������� ������" )]
    private TypeWeapons _typeWeapon;
    /// <summary>
    /// ���������� ���������� ������ �� ����� ��� ��������� �����
    /// </summary>
    [SerializeField, Tooltip( "���������� ���������� ������ �� ����� ��� ��������� �����" ), Range( 1 , 19 )]
    private int _occupiedArea;

    [SerializeField, Tooltip( "��������� �����" )]
    private int _cost;

    [SerializeField, Tooltip( "������ ������ ��� ������������ � ����������" )]
    private Sprite _sprite;
    /// <summary>
    /// �������� ��� �����
    /// </summary>
    public string GetName => _name;
    /// <summary>
    /// ��������� ������� ����� ��� ���������
    /// </summary>
    public float GetLuck => _luck;
    /// <summary>
    /// �������� ��������� �����
    /// </summary>
    public float GetDistance => _distance;
    /// <summary>
    /// �������� ��� ������������� ������
    /// </summary>
    public TypeWeapons GetTypeWeapons => _typeWeapon;
    /// <summary>
    /// �������� ���������� ���������� ������ �� ����
    /// </summary>
    public int GetOCcupiedArea => _occupiedArea;
}
