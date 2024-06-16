using Unity.VisualScripting;
using UnityEngine;

public abstract class StateComponent  
{
    protected UnitsEngine _engine;

    public StateComponent( UnitsEngine engine )
    {
        _engine = engine;   
    }

    
  
}
