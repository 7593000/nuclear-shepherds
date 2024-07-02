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
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.ATTACK  );//todo=> переправить на ATtack
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.DIRECT);


       // unit.StartAnimation.ToRun(StateUnit.ATTACK);
       


        //
        //unit.GetTarget.position

    }

    public void ExitState(UnitComponent unit)
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit( unit , StateUnitList.DIRECT );
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.ATTACK);//todo=> переправить на ATtack
      

       
    }

    public void UpdateState(UnitComponent unit)
    {

        if (unit.GetTargetForAttack != null)
        {
             
            _attack.Attack();
             
        }
        else
        {
            unit.SetState(unit.IdleState);
        }



    }




}
