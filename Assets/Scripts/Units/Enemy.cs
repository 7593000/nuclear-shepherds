using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitComponent, IHealth, IMovable

{
    private Protection _protection;
    [SerializeField] private bool _busyWave = false;
    public static event Action<int> OnDeath;

    private List<IReset> _resetDataComponents = new();
     
    /// <summary>
    /// Проверка врага: Свободен ли он для добавление в список волны.
    /// </summary>
    public bool BusyWave { get => _busyWave; set => _busyWave = value; }

   

    public bool IsDead { get; set; }

    public void Move()
    {

        float step = _config.GetSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GetTarget.position, step);

    }

    public void TakeDamage(TypeWeapons type, float damage)
    {


        if (!IsDead)
        {

            float damageProtect = Mathf.Max(0, damage - _protection.CalculationProtection(type));

            float health = _health.TakeDamage(damageProtect);

            if (health <= 0)
            {
                DeactiveUnit();

                OnDeath?.Invoke(GetCost);

            }
        }



    }



    public float Health() => _config.GetHealth;



    public override void DeactiveUnit()
    {
        base.DeactiveUnit();
        ResetUnit();

    }

    protected override void Initialized()
    {
        base.Initialized();
        _health.Container(this);
        _protection = new Protection(this);
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
        BusyWave = false;
        IsDead = false;



    }


    private void OnEnable()
    {
        SetState(MoveState);
    }
    private void CollectResettableComponents()
    {
        IReset[] resettableComponents = GetComponentsInChildren<IReset>();
        _resetDataComponents.AddRange(resettableComponents);

    }
}

