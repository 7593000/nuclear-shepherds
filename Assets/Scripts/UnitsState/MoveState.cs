/// <summary>
/// Состояние : Движение к цели
/// </summary>
public class MoveState : IUnitState
{
    //public MoveState(GameHub gameHub) : base(gameHub) { }

    private IMovable _movable;
    private int _goalCount;
    private float _activeDistance = 1.2f;
    private float _activeDistanceSqr;
    private TargetPoint _targetPoint;

    public void EnterState(UnitComponent unit)
    {
        _movable = unit.GetComponent<IMovable>();

        if (unit.GetGameHub.GetPointsTarget.GetTargets.Count == 0)
        {
            unit.SetState(unit.NoneState);
            return;
        }
        _goalCount = unit.GetGameHub.GetPointsTarget.GetTargets.Count;
        _activeDistanceSqr = _activeDistance * _activeDistance;

        _targetPoint = unit.GetGameHub.GetPointsTarget.GetTargets[unit.GetSelectedGoal];

        unit.GetTarget = _targetPoint.transform;

        unit.GetDirectionView = _targetPoint.GetAngleForanimation;

        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.MOVE);

        unit.StartAnimation.ToRun(StateUnit.MOVE);
    }

    public void ExitState(UnitComponent unit)
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.MOVE);
        unit.StartAnimation.ToRun(StateUnit.IDLE);
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
                _targetPoint = unit.GetGameHub.GetPointsTarget.GetTargets[unit.GetSelectedGoal];
                unit.GetTarget = _targetPoint.transform;
                // unit.GetTarget = unit.GetGameHub.GetPointsTarget.GetTargets[ unit.GetSelectedGoal ].transform;
                unit.GetDirectionView = _targetPoint.GetAngleForanimation;
                unit.StartAnimation.ToRun(StateUnit.MOVE);
                // GameHub.Logger( unit.GetDirectionView[0]+" :: " + unit.GetDirectionView[1] );
            }
            else
            {
                unit.SetState(unit.SearchState);
            }

        }
        _movable.Move();




    }
}
