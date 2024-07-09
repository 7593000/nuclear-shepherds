using System.Collections.Generic;
using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{
    protected UnitData _unitData;
    //private List<IReset> _resetDataComponents = new();

    [SerializeField] protected Animator _animator;
    private AnimatorComponent _animatorComponent;
    [SerializeField] protected UnitConfig _config;
    [SerializeField] public Health _health;
    [SerializeField] protected GameHub _gameHub;


    [SerializeField] private Damage _damage;
    [SerializeField] protected IAttack _attack;
    public GameHub GetGameHub => _gameHub;
    public IAttack GetAttack => _attack;
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
   /// ������� �� ������ 
   /// </summary>
    public Vector3Int CellPosition;
    /// <summary>
    /// ����������� �������� [-1;0;1]
    /// </summary>
    public int[] GetDirectionView { get; set; } = new int[2];

    //protected StateUnit GetStateUnit => StateUnit.IDLE;

    /// <summary>
    /// ������� ��������� �����
    /// </summary>
    public IUnitState CurrentState { get; private set; }




    public IUnitState NoneState { get; private set; } //������������ ���������
    public IUnitState IdleState { get; private set; }    //�����������
    public IUnitState MoveState { get; private set; } // ��������� 
    public IUnitState AttackState { get; private set; } //���������
    public IUnitState SearchState { get; private set; } //����� �����(�������)
    public IUnitState DeadState { get; private set; }   // ������

 
    public virtual void Initialized( GameHub gameHub )
    {
        _gameHub = gameHub;
        AddComponentsUnit();

    }
    protected virtual void AddComponentsUnit()
    {
        if ( _gameHub == null )
            _gameHub = FindObjectOfType<GameHub>();

        _unitData = new UnitData( GetConfig );

        NoneState = new NoneState();
        IdleState = new IdleState();
        MoveState = new MoveState();
        AttackState = new AttackState();
        SearchState = new SearchState();
        DeadState = new DeadState();

        _animator = GetComponent<Animator>();

        _damage = new Damage( GetConfig.GetWeaponsConfig , _unitData.Luck + _unitData.LuckRatio );

        TypeWeapons typeWeapon = GetConfig.GetWeaponsConfig.GetTypeWeapons;

        _attack = WeaponFactory.CreateWeapon( typeWeapon , this );


        _animatorComponent = gameObject.AddComponent<AnimatorComponent>();
        _animatorComponent.Container( this );


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
    /// <summary>
    /// �������� ����� 
    /// </summary>
    public virtual void DeactiveUnit()
    {
    
        SetState( NoneState );
        gameObject.SetActive(false);
        
        Debug.Log("���� ���������");
        //TODO=> ���� �������� ������
    }

   
  
 
    public void UpdateLevel(int level = 1)
    {

        _unitData.Level = level;

        if (TryGetComponent(out Friends friends))
        {
            int costUpdate = ( int )GetConfig.GetRatio[ 0 ] * GetUnitData.Level ;
            _gameHub.GetWalletEngine.GetWallet.TakeCurrency( costUpdate );


            _unitData.DamageRatio += GetConfig.GetRatio[1] * _unitData.Level  ;
            ;  
            _unitData.SpeedAttackRatio += GetConfig.GetRatio[2] * _unitData.Level  ;
            ;  
            _unitData.LuckRatio += GetConfig.GetRatio[ 3 ] * _unitData.Level;
            ; 

            _damage.SetLuck(_unitData.Luck + _unitData.LuckRatio);
            _damage.SetDamage(_unitData.Damage + _unitData.DamageRatio);
            _damage.SetSpeedAttack(Mathf.Max(0, _unitData.SpeedAttack - _unitData.SpeedAttackRatio));
             

            Sprite levelSprite = _gameHub.GetGameSettings.GetSpriteLevel(_unitData.Level);
             
           
            friends.SetSpriteLevel(levelSprite);
        }
        else
        {
            Debug.LogError("Error Friends �� ������");
        }
        Debug.Log($"Level: {_unitData.Level}");
        Debug.Log($"DamageRatio: {_unitData.DamageRatio}, SpeedAttackRatio: {_unitData.SpeedAttackRatio}, LuckRatio: {_unitData.LuckRatio}");
    
    
    
    }

}/// <summary>
/// ����� ��� ������������� �����, �������� ����� � ����� ( ��� ��������� ������ ) 
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


