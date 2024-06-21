/// <summary>
/// Состояние: бездействие 
/// </summary>
public class IdleState : IUnitState
{
    public void EnterState( UnitComponent unit )
    {
        unit.StartAnimation.ToRun(StateUnit.IDLE);
    }

    public void ExitState( UnitComponent unit )
    {
      
    }

    public void UpdateState( UnitComponent unit )
    {
        
    }
}
