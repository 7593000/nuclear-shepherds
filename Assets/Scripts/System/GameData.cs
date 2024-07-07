using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] List<UnitConfig> _configEnemyUnit = new();
    [SerializeField] List<UnitConfig> _configFriendUnit = new();
    [SerializeField] private Sprite[] _spriteLevel;
    
    /// <summary>
    /// Получить список конфигов дружеский юнитов
    /// </summary>
    public IReadOnlyList<UnitConfig> GetFriendsConfigs => _configFriendUnit;

    /// <summary>
    /// Получить список конфигов вражеских
    /// </summary>
    public IReadOnlyList<UnitConfig> GetEnemiesConfigs => _configEnemyUnit;

    /// <summary>
    /// Взять максимальный возможный уровень 
    /// </summary>
    public int GetMaxLevel => _spriteLevel.Length;
    /// <summary>
    /// Взять спрайт уровня
    /// </summary>
    public Sprite GetSpriteLevel(int index)
    {
        if(index-1 <= _spriteLevel.Length)
        return _spriteLevel[index-1];

        return null;
    }  
}
