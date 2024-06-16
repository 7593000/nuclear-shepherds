using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{
    [SerializeField]
    protected UnitConfig _config; 
  
    public IUnitState CurrentState { get; private set; }


    public IUnitState IdleState { get; private set; }
    public IUnitState MoveState { get; private set; }
    public IUnitState AttackState { get; private set; }
    public IUnitState SearchState { get; private set; }
    public IUnitState DeadState { get; private set; }

    public abstract void Move();

   public void Container(UnitsEngine  engine)
    {
     
        IdleState = new IdleState();
        MoveState = new MoveState( engine );
        AttackState = new AttackState( engine );
        SearchState = new SearchState( engine );
        DeadState = new DeadState();

    }
    //TODO=>TEMP
    private void Awake()
    {
        Container( FindObjectOfType<UnitsEngine>() );
        SetState(MoveState);
    }

    public void SetState( IUnitState newState )
    {
        CurrentState?.ExitState( this );
        CurrentState = newState;
        CurrentState?.EnterState( this );

    }


    public void UpdateUnit()
    {
        if ( gameObject.activeSelf )
        {
            CurrentState?.UpdateState( this );
        }
    }



}
