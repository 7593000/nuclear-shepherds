using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{
    [SerializeField]
    protected UnitConfig _config;
    [SerializeField]
    protected GameHub _gameHub;
    /// <summary>
    /// ��������� ���� ��� �������� : �������� � ����; ����� ����;
    /// </summary>
    public int GetSelectedGoal;
    public Transform GetTarget;
    
    protected StateUnit GetStateUnit => StateUnit.IDLE;
    public IUnitState CurrentState { get; private set; }

    public IUnitState NoneState { get; private set; } //������������ ���������
    public IUnitState IdleState { get; private set; }    //�����������
    public IUnitState MoveState { get; private set; } // ��������� 
    public IUnitState AttackState { get; private set; } //���������
    public IUnitState SearchState { get; private set; } //����� �����(�������)
    public IUnitState DeadState { get; private set; }   // ������

    public abstract void Move();

    public void Container(GameHub gameHub)
    {
        _gameHub = gameHub;
        NoneState = new NoneState(gameHub);
        IdleState = new IdleState();
        MoveState = new MoveState(gameHub );
        AttackState = new AttackState(gameHub);
        SearchState = new SearchState(gameHub);
        DeadState = new DeadState();

    }
    //TODO=>TEMP
    private void Awake()
    {
        Container(FindObjectOfType<GameHub>());
        SetState(MoveState);
    }

    public void SetState(IUnitState newState)
    {
        CurrentState?.ExitState(this);
        CurrentState = newState;
        CurrentState?.EnterState(this);

    }


    public void UpdateUnit()
    {
        if (gameObject.activeSelf)
        {
            CurrentState?.UpdateState(this);
        }
    }



}
