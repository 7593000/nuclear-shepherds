using System.Diagnostics;
using static UnityEngine.GraphicsBuffer;

public class NoneState :StateComponent, IUnitState
{
    public NoneState(GameHub engine) : base(engine) { }

    public void EnterState(UnitComponent unit)
    {
        
       


    }

    public void ExitState(UnitComponent unit)
    {
         

    }

    public void UpdateState(UnitComponent unit)
    {
        
    }
}