using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{
    [SerializeField]
    private UnitConfig _config;
    private float _health;
    private float _speed;

    public IUnitState CurrentState { get; private set; }//состояние юнита


    public IUnitState IdleState { get; private set; }
    public IUnitState MoveState { get; private set; }
    public IUnitState AttackState { get; private set; }
    public IUnitState SearchState { get; private set; }
    public IUnitState DeadState { get; private set; }

    protected virtual void Awake()
    {

        IdleState = new IdleState();
        MoveState = new MoveState();
        AttackState = new AttackState();
        SearchState = new SearchState();
        DeadState = new DeadState();

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
