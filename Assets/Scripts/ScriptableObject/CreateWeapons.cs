using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu( fileName = "_weapon" , menuName = "Configuration/Weapons" , order = 1 )]
public class CreateWeapons : ScriptableObject
{
    /// <summary>
    /// ��� ������
    /// </summary>
  [SerializeField, Tooltip("��� ������")] private TypeWeapons _type;
    /// <summary>
    /// �������� �����
    /// </summary>
    [SerializeField, Tooltip( "�������� �����" )] private float _speedAttack;
    /// <summary>
    /// ��������� ����
    /// </summary>
    [SerializeField, Tooltip( "��������� ����" )] private float _damage;
 
  [SerializeField, Tooltip( "������ ������ ��� ������������ � ����������" )]  private Sprite _sprite;
    public float GetSpeedAttack => _speedAttack;
    public float GetDamage => _damage;
    public Sprite GetSprite => _sprite;
}
