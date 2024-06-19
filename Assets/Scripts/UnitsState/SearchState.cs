using UnityEngine;


/// <summary>
/// Состояние : Поиск цели ( поиск брамина для атаки ) 
/// </summary>
public class SearchState : IUnitState
{

    IMovable _movable;
    Brahmin _brahmin;


    private float _activeDistanceSqr;

    public void EnterState(UnitComponent unit)
    {
        _movable = unit.GetComponent<IMovable>();
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.MOVE);
        _activeDistanceSqr = unit.GetDistance * unit.GetDistance;
        _brahmin = SearchTargetAttack(unit);

        if (_brahmin == null) { 
            unit.SetState(unit.NoneState); 
            return; 
        }


        unit.GetTarget = _brahmin.transform;
        unit.GetTargetForAttack = _brahmin;


    }

    public void ExitState(UnitComponent unit)
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.MOVE);

    }

    public void UpdateState(UnitComponent unit)
    {
        if (!_brahmin.gameObject.activeSelf)
        {
            _brahmin = SearchTargetAttack(unit);

            if (_brahmin == null) { unit.SetState(unit.NoneState); return; }
        }

        float distanceSquared = (unit.transform.position - unit.GetTarget.position).sqrMagnitude;


        if (distanceSquared < _activeDistanceSqr)
        {
            unit.SetState(unit.AttackState);
        }
        _movable.Move();
    }





    private Brahmin SearchTargetAttack(UnitComponent unit)
    {
        float closestDistanceSqr = Mathf.Infinity;
        Brahmin closestBrahmin = null;
        Vector3 unitPosition = unit.transform.position;

        foreach (Brahmin brahmin in unit.GetGameHub.GetBrahmin.GetBrahminList)
        {
            Vector3 directionToTarget = brahmin.transform.position - unitPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestBrahmin = brahmin;
            }


        }

        return closestBrahmin != null ? closestBrahmin : null;
    }
}
