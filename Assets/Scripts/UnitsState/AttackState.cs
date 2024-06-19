/// <summary>
/// Состояние: Атака цели
/// </summary>
public class AttackState : IUnitState
{
    private IAttack _attack;
    public void EnterState( UnitComponent unit )
    {
        _attack = unit.GetComponent<IAttack>();
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit( unit , StateUnitList.OTHER );

    }

    public void ExitState( UnitComponent unit )
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit( unit , StateUnitList.OTHER );
    }

    public void UpdateState( UnitComponent unit )
    {

        if ( unit.GetTargetForAttack.IsDead )
        {
            unit.SetState( unit. NoneState );
            unit.gameObject.SetActive( false );  
        }

        _attack.Attack();

    }




}
