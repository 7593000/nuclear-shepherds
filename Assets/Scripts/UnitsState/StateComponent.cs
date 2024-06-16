using UnityEngine;

public abstract class StateComponent : MonoBehaviour
{
    protected UnitsEngine _engine;

    public void Container( UnitsEngine engine )
    {

        _engine = engine;
    }
 
}
