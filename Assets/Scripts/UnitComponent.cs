using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{
    protected UnitData _unitData;

    [SerializeField] private Animator _animator;
    private AnimatorComponent _animatorComponent;
    [SerializeField] protected UnitConfig _config;
    [SerializeField] public Health _health;
    [SerializeField] protected GameHub _gameHub;


    private Damage _damage;

    public GameHub GetGameHub => _gameHub;
    // public Weapon GetWeapons => _weapons;
    /// <summary>
    /// �������� ������ �� ����� Damage
    /// </summary>
    public Damage GetDamageClass => _damage;
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
    public UnitData GetUnitData => _unitData;
    public UnitConfig GetConfig => _config;
    public TypeUnit GetTypeUnit => GetConfig.GetTypeUnit;
    public Animator GetAnimator => _animator;
    public AnimatorComponent StartAnimation => _animatorComponent;

    public int GetCost => GetConfig.GetCost;

    /// <summary>
    /// ����������� �������� [-1;0;1]
    /// </summary>
    public int[] GetDirectionView { get; set; } = new int[2];

    //protected StateUnit GetStateUnit => StateUnit.IDLE;

    /// <summary>
    /// ������� ��������� �����
    /// </summary>
    public IUnitState CurrentState { get; private set; }

    public float GetDistance { get; private set; }


    public IUnitState NoneState { get; private set; } //������������ ���������
    public IUnitState IdleState { get; private set; }    //�����������
    public IUnitState MoveState { get; private set; } // ��������� 
    public IUnitState AttackState { get; private set; } //���������
    public IUnitState SearchState { get; private set; } //����� �����(�������)
    public IUnitState DeadState { get; private set; }   // ������

    public virtual void Container(GameHub gameHub)
    {


        _gameHub = gameHub;
    }
    protected virtual void Initialized()
    {

        _unitData = new UnitData(GetConfig, 1, 1, 1);

        GetDistance = GetConfig.GetDistance;

        NoneState = new NoneState();
        IdleState = new IdleState();
        MoveState = new MoveState();
        AttackState = new AttackState();
        SearchState = new SearchState();
        DeadState = new DeadState();

        _damage = new Damage(GetConfig.GetWeaponsConfig, _unitData.Luck);

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


}/// <summary>
/// ����� ��� ������������� �����, �������� ����� � ����� ( ��� ��������� ������ ) 
/// </summary>
[System.Serializable]
public struct UnitData
{
    public UnitData(UnitConfig config, float damageRatio, float speedAttackRatio, float luckRatio)
    {
        Level = 1;

        Damage = config.GetWeaponsConfig.GetDamage;
        SpeedAttack = config.GetWeaponsConfig.GetSpeedAttack;
        Luck = config.GetLuck;

        DamageRatio = damageRatio;
        SpeedAttackRatio = speedAttackRatio;
        LuckRatio = luckRatio;

    }
    public int Level { get; set; }

    public float Damage { get; private set; }
    public float SpeedAttack { get; private set; }
    public float Luck { get; private set; }
    public float DamageRatio { get; set; }
    public float SpeedAttackRatio { get; set; }
    public float LuckRatio { get; set; }

}


