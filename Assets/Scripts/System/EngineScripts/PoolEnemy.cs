using System.Collections.Generic;
using UnityEngine;

public sealed class PoolEnemy : MonoBehaviour
{
    private GameHub _gameHub;

    [SerializeField] private Transform _parent;
    [SerializeField] private int _countEnemy = 20;
    private Dictionary<UnitConfig, List<UnitComponent>> _pool = new();

    public void Initialized(GameHub gameHub)
    {
        _gameHub = gameHub;
        CreatePoolEnemy(_countEnemy);
    }

    /// <summary>
    /// Собрать пул для вражеских юнитов
    /// </summary>
    private void CreatePoolEnemy(int count)
    {
        foreach (UnitConfig config in _gameHub.GetGameSettings.GetEnemiesConfigs)
        {
            _pool.Add(config, new List<UnitComponent>());

            for (int i = 0; i < count; i++)
            {
                Enemy enemy = InstantiateEnemy(config);
                if (enemy != null)
                {
                    _pool[config].Add(enemy);
                }
            }
        }
    }

    /// <summary>
    /// Создать врага и добавить его в пул
    /// </summary>
    /// <param name="unitConfig">Конфигурация юнита</param>
    /// <returns>Созданный юнит</returns>
    private Enemy InstantiateEnemy(UnitConfig unitConfig)
    {
        Debug.Log("Создан +1 юнит");
        UnitComponent unit = Instantiate(unitConfig.GetPrefab, _parent);

        Enemy enemyUnit = unit as Enemy;
        if (enemyUnit == null)
        {
            Debug.LogError("Prefab не является типом Enemy!");
            return null;
        }

        enemyUnit.gameObject.SetActive(false);
        enemyUnit.Container(_gameHub);
        return enemyUnit;
    }

    /// <summary>
    /// Взять врага из пула
    /// </summary>
    /// <param name="config">Конфигурация юнита</param>
    /// <returns>Юнит из пула</returns>
    public Enemy GetEnemy(UnitConfig config)
    {
        if (_pool.TryGetValue(config, out List<UnitComponent> tempList))
        {
            foreach (UnitComponent unit in tempList)
            {
                if (unit.TryGetComponent(out Enemy enemy))
                {
                    if (!enemy.BusyWave && !enemy.gameObject.activeSelf)
                    {
                      
                        return enemy;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("ERROR:  UnitConfig null.");
        }

        Debug.Log("Не найден свободный юнит, создаем новый.");
        Enemy tempUnit = InstantiateEnemy(config);
        if (tempUnit != null)
        {
            _pool[config].Add(tempUnit);
        }

        return tempUnit;
    }
}