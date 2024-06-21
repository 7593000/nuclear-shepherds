using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Состояние: Атака цели
/// </summary>
public class AttackState : IUnitState
{
    private IAttack _attack;
    public void EnterState(UnitComponent unit)
    {
        _attack = unit.GetComponent<IAttack>();
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.OTHER);//todo=> переправить на ATtack
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.DIRECT);


        unit.StartAnimation.ToRun(StateUnit.ATTACK);
       


        //
        //unit.GetTarget.position

    }

    public void ExitState(UnitComponent unit)
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.OTHER);//todo=> переправить на ATtack
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.DIRECT);

       
    }

    public void UpdateState(UnitComponent unit)
    {

        if (unit.GetTargetForAttack != null)
        {
          //  unit.GetDirectionView = unit.GetGameHub.GetPointsTarget.GetTargets[unit.GetSelectedGoal].GetAngleForanimation;
            
            _attack.Attack();
            //unit.gameObject.SetActive( false );  
        }
        else
        {
            unit.SetState(unit.IdleState);
        }



    }




}
