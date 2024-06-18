using UnityEngine;

/// <summary>
/// Состояние: Атака цели
/// </summary>
public class AttackState :  IUnitState
{
     

    public void EnterState( UnitComponent unit )
    {

       // _gameHub.GetUnitsUpdateEngine.AddUnit( unit , StateUnitList.OTHER );
    }

    public void ExitState( UnitComponent unit )
    {
       // _gameHub.GetUnitsUpdateEngine.RemoveUnit( unit , StateUnitList.OTHER );
    }

    public void UpdateState( UnitComponent unit )
    {
        throw new System.NotImplementedException();
    }



    
}
