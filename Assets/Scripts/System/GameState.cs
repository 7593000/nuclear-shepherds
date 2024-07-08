using System.Collections.Generic;
using UnityEngine;
 
public class GameState : MonoBehaviour
{
    private static GameState _instance;
    private GameData _gameData;
    public static GameState Instance
    {
        get
        {
            if ( _instance == null )
            {
                GameObject gameOb = new GameObject( "GameState" );
                _instance = gameOb.AddComponent<GameState>();
                DontDestroyOnLoad( gameOb );
            }
            return _instance;
        }
    }

    public bool IsLoading { get; set; } = false;
    public GameData LoadedGameData { get;   set ; }
}
 