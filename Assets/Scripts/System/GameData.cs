using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] List<UnitConfig> _configEnemyUnit = new();
    [SerializeField] List<UnitConfig> _configFriendUnit = new();
    [SerializeField] private Sprite[] _spriteLevel;
    
    /// <summary>
    /// �������� ������ �������� ��������� ������
    /// </summary>
    public IReadOnlyList<UnitConfig> GetFriendsConfigs => _configFriendUnit;

    /// <summary>
    /// �������� ������ �������� ���������
    /// </summary>
    public IReadOnlyList<UnitConfig> GetEnemiesConfigs => _configEnemyUnit;

    /// <summary>
    /// ����� ������������ ��������� ������� 
    /// </summary>
    public int GetMaxLevel => _spriteLevel.Length;
    /// <summary>
    /// ����� ������ ������
    /// </summary>
    public Sprite GetSpriteLevel(int index)
    {
        if(index-1 <= _spriteLevel.Length)
        return _spriteLevel[index-1];

        return null;
    }  
}
