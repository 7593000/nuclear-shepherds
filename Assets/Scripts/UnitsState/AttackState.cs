using UnityEngine;

/// <summary>
/// Состояние: Атака цели
/// </summary>
public class AttackState :  IUnitState
{

    IAttack _attack;
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
     
        _attack.Attack();

    }



    
}
