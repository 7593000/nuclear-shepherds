/// <summary>
/// ��������� : ����� ���� ( ����� ������� ��� ����� ) 
/// </summary>
public class SearchState : StateComponent, IUnitState
{
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
