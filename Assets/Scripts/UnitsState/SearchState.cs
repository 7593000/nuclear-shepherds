using UnityEngine;


/// <summary>
/// Состояние : Поиск цели ( поиск брамина для атаки ) 
/// </summary>
public class SearchState : IUnitState
{

    //public SearchState(GameHub gameHub) : base(gameHub)
    //{
    //}

    public void EnterState(UnitComponent unit)
    {
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.MOVE);

        Brahmin  brahmin = SearchTargetAttack(unit);
        unit.GetTarget = brahmin.transform;
        unit.GetTargetForAttack = brahmin;
        if (unit.GetTarget == null) { unit.SetState(unit.NoneState); }
    }

    public void ExitState(UnitComponent unit)
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.MOVE);
 
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

        return closestBrahmin != null? closestBrahmin : null;
    }
}
