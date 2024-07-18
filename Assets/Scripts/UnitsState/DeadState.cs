/// <summary>
/// Состояние: Цель мертва
/// </summary>
public class DeadState : IUnitState
{
    public void EnterState( UnitComponent unit )
    {
        unit.GetAnimator.SetTrigger( "Dead" );
        if ( unit.GetConfig.GetSoundDead )
            SoundEngine.Instance.PlaySound( unit.GetConfig.GetSoundDead , SoundType.SFXPlayOne, unit.transform );
    }

    public void ExitState( UnitComponent unit )
    {
       
    }

    public void UpdateState( UnitComponent unit )
    {
        
    }
}
