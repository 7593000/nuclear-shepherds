/// <summary>
/// Состояние: Атака цели
/// </summary>
public class AttackState : IUnitState
{
    private IAttack _attack;
    public void EnterState( UnitComponent unit )
    {
        _attack = unit.GetComponent<IAttack>();
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit( unit , StateUnitList.ATTACK );
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit( unit , StateUnitList.DIRECT );


    }

    public void ExitState( UnitComponent unit )
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit( unit , StateUnitList.DIRECT );
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit( unit , StateUnitList.ATTACK );

    }

    public void UpdateState( UnitComponent unit )
    {
        if ( unit.GetTargetForAttack != null )
        {

            float damage = unit.GetDamageClass.DamageTarget();

            if ( damage >= 0 )
            {


                unit.StartAnimation.ToRun( StateUnit.ATTACK );

                unit.Attack.Attack(damage);
                 
            }
           

        }
        else
        {
            unit.SetState( unit.IdleState );
        }




    }




}
