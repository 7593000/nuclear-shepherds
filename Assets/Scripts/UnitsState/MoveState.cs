/// <summary>
/// ��������� : �������� � ����
/// </summary>
public class MoveState : StateComponent, IUnitState
{
    public MoveState( UnitsEngine engine ) : base( engine )
    {
    }

    public void EnterState( UnitComponent unit )
    {
      _engine.AddUnit(unit, StateUnitList.MOVE);
     
    }

    public void ExitState( UnitComponent unit )
    {
        _engine.RemoveUnit( unit , StateUnitList.MOVE );
    }

    public void UpdateState( UnitComponent unit )
    {
        unit.Move();
    }
}
