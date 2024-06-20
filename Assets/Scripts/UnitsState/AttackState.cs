/// <summary>
/// Состояние: Атака цели
/// </summary>
public class AttackState : IUnitState
{
    private IAttack _attack;
    public void EnterState(UnitComponent unit)
    {
        _attack = unit.GetComponent<IAttack>();
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit(unit, StateUnitList.OTHER);

    }

    public void ExitState(UnitComponent unit)
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit(unit, StateUnitList.OTHER);
    }

    public void UpdateState(UnitComponent unit)
    {

        if (unit.GetTargetForAttack != null)
        {

            _attack.Attack();
            //unit.gameObject.SetActive( false );  
        }
        else
        {
            unit.SetState(unit.IdleState);
        }



    }




}
