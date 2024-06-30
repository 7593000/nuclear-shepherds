using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] List<UnitConfig> _configEnemyUnit = new();
    [SerializeField] List<UnitConfig> _configFriendUnit = new();
    /// <summary>
    /// �������� ������ �������� ��������� ������
    /// </summary>
    public IReadOnlyList<UnitConfig> GetFriendsConfigs => _configFriendUnit;

    /// <summary>
    /// �������� ������ �������� ���������
    /// </summary>
    public IReadOnlyList<UnitConfig> GetEnemiesConfigs => _configEnemyUnit;
}
