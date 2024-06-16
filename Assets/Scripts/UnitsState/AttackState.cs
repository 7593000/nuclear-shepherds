/// <summary>
/// Состояние: Атака цели
/// </summary>
public class AttackState : StateComponent, IUnitState
{
    public AttackState( UnitsEngine engine ) : base( engine )
    {
    }

    public void EnterState( UnitComponent unit )
    {

        _engine.AddUnit( unit , StateUnitList.OTHER );
    }

    public void ExitState( UnitComponent unit )
    {
        _engine.RemoveUnit( unit , StateUnitList.OTHER );
    }

    public void UpdateState( UnitComponent unit )
    {
        throw new System.NotImplementedException();
    }
}
