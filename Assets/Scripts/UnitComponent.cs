using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] protected UnitConfig _config;
    [SerializeField] public Health _health;
    [SerializeField] protected GameHub _gameHub;
    //  private Weapon _weapons;
    private Attack _attack;
    public GameHub GetGameHub => _gameHub;
    // public Weapon GetWeapons => _weapons;
    public Attack GetAttack => _attack;
    /// <summary>
    /// ���� ����� ��� ����� 
    /// </summary>
    public IHealth GetTargetForAttack;
    /// <summary>
    /// ��������� ���� ��� �������� : ������ ���� � ������  TODO=> ��������� ����� � ����� ������
    /// </summary>
    public int GetSelectedGoal;
    /// <summary>
    /// ����� ���� � �����
    /// </summary>
    public Transform GetTarget;

    public Animator GetAminator => _animator;

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

        Luck = _config.GetLuck;

        GetDistance = _config.GetDistance;

        NoneState = new NoneState();
        IdleState = new IdleState();
        MoveState = new MoveState();
        AttackState = new AttackState();
        SearchState = new SearchState();
        DeadState = new DeadState();
        _attack = new Attack(_config.GetWeaponsConfig, Luck);

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
        _animator = GetComponent<Animator>();

    }
    private void Start()
    {
        Initialized();
        SetState(MoveState);
    }


}
