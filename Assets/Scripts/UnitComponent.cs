using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    private AnimatorComponent _animatorComponent;
    [SerializeField] protected UnitConfig _config;
    [SerializeField] public Health _health;
    [SerializeField] protected GameHub _gameHub;
    //  private Weapon _weapons;
    private Damage _damage;
    public GameHub GetGameHub => _gameHub;
    // public Weapon GetWeapons => _weapons;
    public Damage GetAttack => _damage;
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
    public TypeUnit GetTypeUnit => _config.GetTypeUnit;
    public Animator GetAnimator => _animator;
    public AnimatorComponent StartAnimation => _animatorComponent;
    /// <summary>
    /// ����������� �������� [-1;0;1]
    /// </summary>
    public int[] GetDirectionView { get; set; } = new int[2];

    //protected StateUnit GetStateUnit => StateUnit.IDLE;

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
        _damage = new Damage(_config.GetWeaponsConfig, Luck);

        _animatorComponent = gameObject.AddComponent<AnimatorComponent>();
        _animatorComponent.Container(this);


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
    protected virtual void Start()
    {
        Initialized();
       
    }


}
