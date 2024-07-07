using UnityEngine;
//TODO=> DEL

public class GameState : MonoBehaviour
{
    private static GameState _instance;

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

    public   bool IsLoading { get; private set; }
}
