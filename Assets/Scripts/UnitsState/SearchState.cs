/// <summary>
/// Состояние : Поиск цели ( поиск брамина для атаки ) 
/// </summary>
public class SearchState : StateComponent, IUnitState
{
    public SearchState( UnitsEngine engine ) : base( engine )
    {
    }

    public void EnterState( UnitComponent unit )
    {
        _engine.AddUnit(unit, StateUnitList.OTHER );
    }

    public void ExitState( UnitComponent unit )
    {
     _engine.RemoveUnit(unit, StateUnitList.OTHER);
    }

    public void UpdateState( UnitComponent unit )
    {
        throw new System.NotImplementedException();
    }
}
