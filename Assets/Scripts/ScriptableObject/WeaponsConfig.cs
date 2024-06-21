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
    [Space]
    [SerializeField,Tooltip("���� ��������")]
    private AudioClip _audioClip;

#if UNITY_EDITOR
    [SerializeField, Tooltip( "������ ������ ��� ������������ � ����������" )]
    private Sprite _sprite;
#endif
    /// <summary>
    /// ����������������� ����������� ������
    /// </summary>
    public float GetDuratuion => _duration;
    /// <summary>
    /// �������� �����
    /// </summary>
    public float GetSpeedAttack => _speedAttack;
    /// <summary>
    /// ����� �����������
    /// </summary>
    public float GetRechargeTime => _rechargeTime;
    public float GetDamage => _damage;

    public AudioClip GetAudioClip => _audioClip;

#if UNITY_EDITOR
    public Sprite GetSprite => _sprite;
#endif
}
