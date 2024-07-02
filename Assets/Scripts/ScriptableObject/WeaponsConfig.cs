using UnityEngine;
[CreateAssetMenu( fileName = "_weapon" , menuName = "Configuration/Weapons" , order = 2 )]
public class WeaponsConfig : ScriptableObject
{
    [SerializeField, Tooltip( "��� ������" )]
    private TypeWeapons _type;
   
 
   
    [SerializeField, Tooltip( "�������� �����" )]
    private float _speedAttack;
    [SerializeField, Tooltip("����� �����������/����� ����� �����")]
    private int _rechargeTime;

    [SerializeField,Tooltip("���������� ����������� � ������")] private int _weaponAmmo;
    [SerializeField, Tooltip( "��������� ����" )]
    private float _damage;
    [SerializeField, Tooltip( "��������� �����" )]
    private float _distance;
    [Space]
    [SerializeField,Tooltip("���� ��������")]
    private AudioClip _audioClip;

#if UNITY_EDITOR
    [SerializeField, Tooltip( "������ ������ ��� ������������ � ����������" )]
    private Sprite _sprite;
#endif
 
    /// <summary>
    /// �������� �����
    /// </summary>
    public float GetSpeedAttack => _speedAttack;
    /// <summary>
    /// ����� �����������
    /// </summary>
    public int GetRechargeTime => _rechargeTime;
    /// <summary>
    /// �������� ���������� ����������� � ������
    /// </summary>
    public int GetWeaponAmmo => _weaponAmmo;    
    /// <summary>
    /// ���� ������
    /// </summary>
    public float GetDamage => _damage;
    /// <summary>
    /// ��������� �����
    /// </summary>
    public float GetDistance => _distance;
    public AudioClip GetAudioClip => _audioClip;

#if UNITY_EDITOR
    public Sprite GetSprite => _sprite;
#endif
}
