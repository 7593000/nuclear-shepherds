using Unity.VisualScripting;
using UnityEngine;

public abstract class StateComponent  
{
    protected GameHub _gameHub;

    public StateComponent(GameHub gameHub)
    {
        _gameHub = gameHub;   
    }

    
  
}
