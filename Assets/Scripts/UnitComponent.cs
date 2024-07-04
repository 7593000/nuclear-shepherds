using System.Collections.Generic;
using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{
    protected UnitData _unitData;
    //private List<IReset> _resetDataComponents = new();

    [SerializeField] private Animator _animator;
    private AnimatorComponent _animatorComponent;
    [SerializeField] protected UnitConfig _config;
    [SerializeField] public Health _health;
    [SerializeField] protected GameHub _gameHub;


    [SerializeField] private Damage _damage;
    [SerializeField] protected IAttack _attack;
    public GameHub GetGameHub => _gameHub;
    public IAttack Attack => _attack;
    /// Получить ссылку на класс Damage
    /// </summary>
    public Damage GetDamageClass => _damage;
    /// <summary>
    /// Цель юнита для атаки 
    /// </summary>
    public IHealth GetTargetForAttack;
    /// <summary>
    /// Выбранная цель для действий : индекс цели в списке  TODO=> перенести метод в класс врагов
    /// </summary>
    public int GetSelectedGoal;
    /// <summary>
    /// Взять цель у юнита
    /// </summary>
    public Transform GetTarget;
    public UnitData GetUnitData => _unitData;
    public UnitConfig GetConfig => _config;
    public TypeUnit GetTypeUnit => GetConfig.GetTypeUnit;
    public Animator GetAnimator => _animator;
    public AnimatorComponent StartAnimation => _animatorComponent;

    public int GetCost => GetConfig.GetCost;
    public Vector3Int CellPosition;
    /// <summary>
    /// направление движения [-1;0;1]
    /// </summary>
    public int[] GetDirectionView { get; set; } = new int[2];

    //protected StateUnit GetStateUnit => StateUnit.IDLE;

    /// <summary>
    /// Текущее состояние юнита
    /// </summary>
    public IUnitState CurrentState { get; private set; }




    public IUnitState NoneState { get; private set; } //Погранничное состояние
    public IUnitState IdleState { get; private set; }    //бездействие
    public IUnitState MoveState { get; private set; } // Двигаться 
    public IUnitState AttackState { get; private set; } //Атаковать
    public IUnitState SearchState { get; private set; } //Поиск врага(брамина)
    public IUnitState DeadState { get; private set; }   // Смерть

    public virtual void Container(GameHub gameHub)
    {
        _gameHub = gameHub;
    }

  
    protected virtual void Initialized()
    {

        _unitData = new UnitData(GetConfig);

        NoneState = new NoneState();
        IdleState = new IdleState();
        MoveState = new MoveState();
        AttackState = new AttackState();
        SearchState = new SearchState();
        DeadState = new DeadState();

        _damage = new Damage(GetConfig.GetWeaponsConfig, _unitData.Luck + _unitData.LuckRatio);

        TypeWeapons typeWeapon = GetConfig.GetWeaponsConfig.GetTypeWeapons;

        _attack = WeaponFactory.CreateWeapon(typeWeapon, this);


        _animatorComponent = gameObject.AddComponent<AnimatorComponent>();
        _animatorComponent.Container(this);

     

    }

    //private void CollectResettableComponents()
    //{
    //    IReset[] resettableComponents = GetComponentsInChildren<IReset>();
    //    _resetDataComponents.AddRange(resettableComponents);
    //    Debug.Log(_resetDataComponents.Count);
    //}


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
    /// <summary>
    /// удаление юнита 
    /// </summary>
    public void DeactiveUnit()
    {
        gameObject.SetActive(false);
        SetState(NoneState);
        Debug.Log("Юнит уничтожен");
        //TODO=> плей анимации смерти
    }

    ////TODO=>TEMP ?
    private void Awake()
    {
        Container(FindObjectOfType<GameHub>());
        _animator = GetComponent<Animator>();


    }



    //public void ResetUnit()
    //{

    //    foreach (IReset resetData in _resetDataComponents)
    //    {
    //        resetData.ResetData();
    //    }

       
    //    GetTargetForAttack = null;
    //    GetSelectedGoal = 0;
        
      
    //    SetState(MoveState);
      



    //}
    //protected virtual void OnEnable()
    //{
    //    ResetUnit();
    //}

    protected virtual void Start()
    {
      // ResetUnit();
        Initialized();

    }

    public void UpdateLevel()
    {
        if (TryGetComponent(out Friends friends))
        {


            _unitData.DamageRatio += GetConfig.GetRatio[1] * _unitData.Level;
            _unitData.SpeedAttackRatio += GetConfig.GetRatio[2] * _unitData.Level;
            _unitData.LuckRatio += GetConfig.GetRatio[3] * _unitData.Level;

            _damage.SetLuck(_unitData.Luck + _unitData.LuckRatio);
            _damage.SetDamage(_unitData.Damage + _unitData.DamageRatio);
            _damage.SetSpeedAttack(Mathf.Max(0, _unitData.SpeedAttack - _unitData.SpeedAttackRatio));

            _unitData.Level += 1;
            friends.SetSpriteLevel(GetGameHub.GetGameData.GetSpriteLevel(_unitData.Level));



        }
    }

}/// <summary>
/// Класс для корректировки урона, скорости атаки и удачи ( при повышении уровня ) 
/// </summary>
[System.Serializable]
public struct UnitData
{
    public UnitData(UnitConfig config)
    {
        Level = 1;
        DamageRatio = 0;
        SpeedAttackRatio = 0;
        LuckRatio = 0;

        Damage = config.GetWeaponsConfig.GetDamage;
        SpeedAttack = config.GetWeaponsConfig.GetSpeedAttack;
        Luck = config.GetLuck;



    }
    public int Level { get; set; }

    public float Damage { get; private set; }
    public float SpeedAttack { get; private set; }
    public float Luck { get; private set; }
    public float DamageRatio { get; set; }
    public float SpeedAttackRatio { get; set; }
    public float LuckRatio { get; set; }

}


