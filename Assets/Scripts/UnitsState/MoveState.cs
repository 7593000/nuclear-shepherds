
/// <summary>
/// Состояние : Движение к цели
/// </summary>
public class MoveState :   IUnitState
{
    //public MoveState(GameHub gameHub) : base(gameHub) { }
   
    private int _goalCount;
    private float _activeDistance = 1.2f;
    private float _activeDistanceSqr;


    public void EnterState(UnitComponent unit)
    {

        if (unit.GetGameHub.GetPointsTarget.GetTargets.Count == 0)
        {
            unit.SetState(unit.NoneState);
            return;
        }
        _goalCount = unit.GetGameHub.GetPointsTarget.GetTargets.Count;
        _activeDistanceSqr = _activeDistance * _activeDistance;
        unit.GetTarget = unit.GetGameHub.GetPointsTarget.GetTargets[unit.GetSelectedGoal].transform;

        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.MOVE);
             
    }

    public void ExitState(UnitComponent unit)
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.MOVE);
    }

    public void UpdateState(UnitComponent unit)
    {


        float distanceSquared = (unit.transform.position - unit.GetTarget.position).sqrMagnitude;
        _activeDistanceSqr = _activeDistance * _activeDistance;

        if (distanceSquared < _activeDistanceSqr)
        {
            if (unit.GetSelectedGoal < _goalCount - 1)
            {

                unit.GetSelectedGoal++;
                unit.GetTarget = unit.GetGameHub.GetPointsTarget.GetTargets[unit.GetSelectedGoal].transform;
            }
            else
            {
                unit.SetState(unit.SearchState);
            }

        }
        unit.Move();




    }
}
