using UnityEngine;


/// <summary>
/// Состояние : Поиск цели ( поиск брамина для атаки ) 
/// </summary>
public class SearchState : StateComponent, IUnitState
{

    public SearchState(GameHub gameHub) : base(gameHub)
    {
    }

    public void EnterState(UnitComponent unit)
    {
        _gameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.MOVE);

        unit.GetTarget = SearchTargetAttack(unit).transform;
 
        if (unit.GetTarget == null) { unit.SetState(unit.NoneState); }
    }

    public void ExitState(UnitComponent unit)
    {
        _gameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.MOVE);
 
    }

    public void UpdateState(UnitComponent unit)
    {   
        unit.Move();
    }


    private Brahmin SearchTargetAttack(UnitComponent unit)
    {
        float closestDistanceSqr = Mathf.Infinity;
        Brahmin closestBrahmin = null;
        Vector3 unitPosition = unit.transform.position;

        foreach (Brahmin brahmin in _gameHub.GetBrahmin.GetBrahminList)
        {
            Vector3 directionToTarget = brahmin.transform.position - unitPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestBrahmin = brahmin;
            }


        }

        return closestBrahmin != null? closestBrahmin : null;
    }
}
