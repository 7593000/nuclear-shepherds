using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{

    [SerializeField] protected UnitConfig _config;
    [SerializeField] public Health _health;
    [SerializeField] protected GameHub _gameHub;
                     private Weapon _weapons;

    public GameHub GetGameHub => _gameHub;
    public Weapon GetWeapons => _weapons;
    /// <summary>
    /// ���� ����� ��� ����� 
    /// </summary>
    public ITakeDamage GetTargetForAttack;
    /// <summary>
    /// ��������� ���� ��� �������� : ������ ���� � ������  TODO=> ��������� ����� � ����� ������
    /// </summary>
    public int GetSelectedGoal;
    /// <summary>
    /// ����� ���� � �����
    /// </summary>
    public Transform GetTarget;

    protected StateUnit GetStateUnit => StateUnit.IDLE;

    public IUnitState CurrentState { get; private set; }

    public float Luck { get; private set; }
    public float GetDistance { get; private set; }


    public IUnitState NoneState { get; private set; } //������������ ���������
    public IUnitState IdleState { get; private set; }    //�����������
    public IUnitState MoveState { get; private set; } // ��������� 
    public IUnitState AttackState { get; private set; } //���������
    public IUnitState SearchState { get; private set; } //����� �����(�������)
    public IUnitState DeadState { get; private set; }   // ������

    protected virtual void Container(GameHub gameHub)
    {

        _gameHub = gameHub;
    }
    protected virtual void Initialized()
    {
        _weapons = new Weapon(_config.GetWeaponsConfig);
        Luck = _config.GetLuck;
        GetDistance = _config.GetDistance;
        NoneState = new NoneState();
        IdleState = new IdleState();
        MoveState = new MoveState();
        AttackState = new AttackState();
        SearchState = new SearchState();
        DeadState = new DeadState();
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
     

    
    //TODO=>TEMP
    private void Awake()
    {
        Container(FindObjectOfType<GameHub>());
     
    }
    private void Start()
    {
        Initialized();
        SetState(MoveState);
    }
  

}
