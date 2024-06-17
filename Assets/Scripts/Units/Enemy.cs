using UnityEngine;

public class Enemy : UnitComponent
{

    private float _activeDistance = 1.2f;


    public override void Move()
    {
         
        var step = _config.GetSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GetTarget.position, step);
       
    }

    private void Start()
    {



        //   SetState( MoveState );
    }
}
