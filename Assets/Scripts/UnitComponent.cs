using UnityEngine;

public abstract class UnitComponent : MonoBehaviour, IAttack , IHealth
{
 
    [SerializeField]
    protected UnitConfig _config;
    [SerializeField] protected WeaponsComponent _weapons;
    [SerializeField]
    protected GameHub _gameHub;
    /// <summary>
    /// ��������� ���� ��� �������� : �������� � ����; ����� ����;
    /// </summary>
    public int GetSelectedGoal;
    public Transform GetTarget;
    public IHealth GetTargetForAttack;
    public GameHub GetGameHub => _gameHub;
    public WeaponsComponent GetWeapons => _weapons;
    protected StateUnit GetStateUnit => StateUnit.IDLE;
   
    public IUnitState CurrentState { get; private set; }

    public float Luck {  get; private set; }    
     public IUnitState NoneState   { get; private set; } //������������ ���������
    public IUnitState IdleState   { get; private set; }    //�����������
    public IUnitState MoveState   { get; private set; } // ��������� 
    public IUnitState AttackState { get; private set; } //���������
    public IUnitState SearchState { get; private set; } //����� �����(�������)
    public IUnitState DeadState   { get; private set; }   // ������
    
    
    public void Attack()
    {
        GetWeapons.Attack(this);

    }

    public void Health(float damage)
    {

    }

    public abstract void Move();

    public void Container(GameHub gameHub)
    {
    
        _gameHub    = gameHub;
        Luck = _config.GetLuck;
        NoneState   = new NoneState();
        IdleState   = new IdleState();
        MoveState   = new MoveState();
        AttackState = new AttackState();
        SearchState = new SearchState();
        DeadState   = new DeadState();

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
