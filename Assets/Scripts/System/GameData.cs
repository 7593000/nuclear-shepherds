using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] List<UnitConfig> _configEnemyUnit = new();
    [SerializeField] List<UnitConfig> _configFriendUnit = new();
    /// <summary>
    /// Получить список конфигов дружеский юнитов
    /// </summary>
    public IReadOnlyList<UnitConfig> GetFriendsConfigs => _configFriendUnit;

    /// <summary>
    /// Получить список конфигов вражеских
    /// </summary>
    public IReadOnlyList<UnitConfig> GetEnemiesConfigs => _configEnemyUnit;
}
