using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Состояние: бездействие 
/// </summary>
public class IdleState : IUnitState
{
    private float _randomTimer = 0;
    private List<int[]> _listPosition = new()
    {
        new[] {  1 ,0 },
        new[] {  1, 1 },
        new[] { -1, 1 },
        new[] { -1, 0 },
        new[] { -1,-1 },
        new[] { -1, 1 }
    };
    public void EnterState( UnitComponent unit )
    {
     
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit( unit , StateUnitList.OTHER );
        unit.StartAnimation.ToRun( StateUnit.IDLE );

        int randomIndex = RandomPosition();
        unit.GetDirectionView[ 0 ] = _listPosition[ randomIndex ][ 0 ];
        unit.GetDirectionView[ 1 ] = _listPosition[ randomIndex ][ 1 ];
        _randomTimer = RandomTimerForIdleAnim();
    }

    public void ExitState( UnitComponent unit )
    {
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit( unit , StateUnitList.OTHER );
     //   SoundEngine.Instance.StopSound( SoundType.SFXPlayOne, unit.GetConfig.GetSoundIdle );
    }

    public void UpdateState( UnitComponent unit )
    {
       
        if ( _randomTimer <= 0 )
        {
          

            int randomIndex = RandomPosition();
            unit.GetDirectionView[ 0 ] = _listPosition[ randomIndex ][ 0 ];
            unit.GetDirectionView[ 1 ] = _listPosition[ randomIndex ][ 1 ];
            unit.StartAnimation.ToRun( StateUnit.IDLE );
           
            SoundEngine.Instance.PlaySound( unit.GetConfig.GetSoundIdle , SoundType.SFXPlayOne, false, unit.transform );
            _randomTimer = RandomTimerForIdleAnim();
        }
        _randomTimer--;
    }


    private float RandomTimerForIdleAnim()
    {
        return Random.Range( 30f , 100f );


    }
    private int RandomPosition()
    {
        return Random.Range( 0 , _listPosition.Count - 1 );


    }

}
