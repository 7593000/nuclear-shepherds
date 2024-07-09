
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Расстановка дружеских юнитов при загрузке игры
/// </summary>
public class UnitPositionEngine : MonoBehaviour
{
    private GameHub _gameHub;


    public void Initialized(GameHub gameHub)
    {
        _gameHub = gameHub;

        if (GameState.Instance.IsLoading) PlacementUnits();
    }
    private void PlacementUnits()
    {
        Debug.Log("Расставляяем юнитов");

        List<UnitComponent> units = new ();  

        Dictionary<int, Dictionary<Vector3Int, int>> tempData = _gameHub.GetGameSettings.GetGameData.UnitsData;

        foreach (KeyValuePair<int, Dictionary<Vector3Int, int>> data in tempData)
        {
            int unitID = data.Key;

            UnitConfig config = _gameHub.GetGameSettings.GetFriendsConfigs.FirstOrDefault(t => unitID == t.GetId);
            if (config != null)
            { 

                 foreach (var unitData in data.Value)
                {
                    Vector3Int positionUnitInt = unitData.Key;
                    int level = unitData.Value;
                   
                    // Создаем и размещаем юнита

                    Vector3 positionUnit = _gameHub.GetTileMap.GetTileMap.CellToWorld(positionUnitInt);


                    UnitComponent friend = Instantiate(config.GetPrefab);
                    friend.transform.position =  positionUnit;
                    friend.CellPosition = positionUnitInt;
                    _gameHub.GetTileMap.AddCell(positionUnitInt);
                    friend.Initialized( _gameHub );
                    units.Add(friend);


                    

                            friend.UpdateLevel( level );

                   


                }
            }
            else
            {
                Debug.Log("Не найден ID в конфигах ");
            }
            
        }
        

        units.ForEach(unit => { _gameHub.GetGameSettings.AddUnit(unit); });

    
    }

}



    //private Enemy InstantiateUnit()
    //{
    //    return;

    //}


