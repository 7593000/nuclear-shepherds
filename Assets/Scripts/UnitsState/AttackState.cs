using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ���������: ����� ����
/// </summary>
public class AttackState : IUnitState
{
    private IAttack _attack;
    public void EnterState(UnitComponent unit)
    {
        _attack = unit.GetComponent<IAttack>();
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.ATTACK  );//todo=> ����������� �� ATtack
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.DIRECT);


       // unit.StartAnimation.ToRun(StateUnit.ATTACK);
       


        //
        //unit.GetTarget.position

    }

    public void ExitState(UnitComponent unit)
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit( unit , StateUnitList.DIRECT );
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.ATTACK);//todo=> ����������� �� ATtack
      

       
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
