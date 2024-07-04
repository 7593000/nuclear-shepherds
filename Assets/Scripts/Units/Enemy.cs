using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitComponent, IHealth,  IMovable 

{
 

    public static event Action<int> OnCoins;
    private List<IReset> _resetDataComponents = new();

    [SerializeField, Tooltip("”ровень противника")] private LevelEnemy _levelEnemy;//TODO => забыл , зачем делал )))

    /// <summary>
    /// ѕроверка врага: —вободен ли он дл€ добавление в список волны.
    /// </summary>
    public bool BusyWave { get; set; } = false;

    public LevelEnemy GetLevelEnemy => _levelEnemy; //TODO => забыл , зачем делал )))

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
                BusyWave = false;


                 OnCoins?.Invoke(GetCost);
               
            }
        }
        else //TODO => DEL? 
        {
            gameObject.SetActive( false );
            BusyWave = false;
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

    protected override void Initialized()
    {
        base.Initialized();
        _health.Container( this );
        CollectResettableComponents();


    }

    protected override void Start()
    {
        base.Start();
        SetState( MoveState ); //TODO=DEL - перенести 
    }
    public void ResetUnit()
    {

        foreach (IReset resetData in _resetDataComponents)
        {
            resetData.ResetData();
        }


        GetTargetForAttack = null;
        GetSelectedGoal = 0;
     
        IsDead = false;

        SetState(MoveState);




    }
    protected virtual void OnEnable()
    {
        ResetUnit();
    }
    private void CollectResettableComponents()
    {
        IReset[] resettableComponents = GetComponentsInChildren<IReset>();
        _resetDataComponents.AddRange(resettableComponents);
       
    }
}

