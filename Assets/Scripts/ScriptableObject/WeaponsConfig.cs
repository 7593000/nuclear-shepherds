using UnityEngine;
[CreateAssetMenu( fileName = "_weapon" , menuName = "Configuration/Weapons" , order = 1 )]
public class WeaponsConfig : ScriptableObject
{
    [SerializeField, Tooltip( "��� ������" )]
    private TypeWeapons _type;
   
    //TODO => DEL
    [SerializeField, Tooltip("����������������� ����������� ������")]
    private float _duration;
   
    [SerializeField, Tooltip( "�������� �����" )]
    private float _speedAttack;
    [SerializeField, Tooltip("����� �����������/����� ����� �����")]
    private float _rechargeTime;

    [SerializeField, Tooltip( "��������� ����" )]
    private float _damage;

#if UNITY_EDITOR
    [SerializeField, Tooltip( "������ ������ ��� ������������ � ����������" )]
    private Sprite _sprite;
#endif
    public float GetSpeedAttack => _speedAttack;
    public float GetDamage => _damage;
    public float GetRechargeTime => _rechargeTime;

#if UNITY_EDITOR
    public Sprite GetSprite => _sprite;
#endif
}
