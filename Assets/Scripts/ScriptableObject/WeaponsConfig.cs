using UnityEngine;
[CreateAssetMenu( fileName = "_weapon" , menuName = "Configuration/Weapons" , order = 1 )]
public class CreateWeapons : ScriptableObject
{
    [SerializeField, Tooltip( "��� ������" )]
    private TypeWeapons _type;

    [SerializeField, Tooltip( "�������� �����" )]
    private float _speedAttack;

    [SerializeField, Tooltip( "��������� ����" )]
    private float _damage;

#if UNITY_EDITOR
    [SerializeField, Tooltip( "������ ������ ��� ������������ � ����������" )]
    private Sprite _sprite;
#endif
    public float GetSpeedAttack => _speedAttack;
    public float GetDamage => _damage;

#if UNITY_EDITOR
    public Sprite GetSprite => _sprite;
#endif
}
