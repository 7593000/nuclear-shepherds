using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitComponent, IHealth,  IMovable 

{
 

    public static event Action<int> OnCoins;
    private List<IReset> _resetDataComponents = new();

    [SerializeField, Tooltip("������� ����������")] private LevelEnemy _levelEnemy;//TODO => ����� , ����� ����� )))

    /// <summary>
    /// �������� �����: �������� �� �� ��� ���������� � ������ �����.
    /// </summary>
    public bool BusyWave { get; set; } = false;

    public LevelEnemy GetLevelEnemy => _levelEnemy; //TODO => ����� , ����� ����� )))

    public bool IsDead { get; set; } = false;

    public void Move()
    {

        float step = _config.GetSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards( transform.position , GetTarget.position , step );

    }

    public void TakeDamage( float damage )
    {


        if ( !IsDead )
        {
            float health = _health.TakeDamage( damage );
          
            if ( health <= 0 )
            {
                DeactiveUnit();
                 
                OnCoins?.Invoke(GetCost);
               
            }
        }
         


    }

    public float Health() => _config.GetHealth;

    //public void Attack()
    //{
    //    float damage = GetDamageClass.DamageTarget();

    //    if (damage >= 0)
    //    {
    //        StartAnimation.ToRun(StateUnit.ATTACK);
    //        GetTargetForAttack.TakeDamage(damage);

    //    }



    //}

    public override void DeactiveUnit()
    {
        base.DeactiveUnit();
        ResetUnit();
    }

    protected override void Initialized()
    {
        base.Initialized();
        _health.Container( this );
        CollectResettableComponents();


    }

    protected override void Start()
    {
        base.Start();
        SetState(MoveState);
    }



    public void ResetUnit()
    {

        foreach (IReset resetData in _resetDataComponents)
        {
            resetData.ResetData();
        }


        GetTargetForAttack = null;
        GetSelectedGoal = 0;
     
       BusyWave= false;
        IsDead = false;

      
          
    }


    private   void OnEnable()
 {
       SetState(MoveState);
  }
    private void CollectResettableComponents()
    {
        IReset[] resettableComponents = GetComponentsInChildren<IReset>();
        _resetDataComponents.AddRange(resettableComponents);
       
    }
}

