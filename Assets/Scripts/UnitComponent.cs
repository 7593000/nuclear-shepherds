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
    /// Цель юнита для атаки 
    /// </summary>
    public ITakeDamage GetTargetForAttack;
    /// <summary>
    /// Выбранная цель для действий : индекс цели в списке  TODO=> перенести метод в класс врагов
    /// </summary>
    public int GetSelectedGoal;
    /// <summary>
    /// Взять цель у юнита
    /// </summary>
    public Transform GetTarget;

    protected StateUnit GetStateUnit => StateUnit.IDLE;

    public IUnitState CurrentState { get; private set; }

    public float Luck { get; private set; }
    public float GetDistance { get; private set; }


    public IUnitState NoneState { get; private set; } //Погранничное состояние
    public IUnitState IdleState { get; private set; }    //бездействие
    public IUnitState MoveState { get; private set; } // Двигаться 
    public IUnitState AttackState { get; private set; } //Атаковать
    public IUnitState SearchState { get; private set; } //Поиск врага(брамина)
    public IUnitState DeadState { get; private set; }   // Смерть

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
