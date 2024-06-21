using System.Collections.Generic;
using UnityEngine;

public class Friends : UnitComponent, IAttack

{
    private LineRenderer _lineRenderer;
    private CircleCollider2D _circleCollider;
    private int _segments = 46;
    private List<UnitComponent> _enemies = new();



    public void Attack()
    {

        GetDamane.DamageTarget(GetTargetForAttack);

    }
    protected override void Start()
    {
        base.Start();


        _lineRenderer = GetComponent<LineRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.radius = _config.GetDistance;
        _lineRenderer.positionCount = _segments + 1;
        _lineRenderer.useWorldSpace = false;
        CreateCircle();
        SetState(IdleState);
    }


    private void CreateCircle()
    {
        float angle = 0f;
        for (int i = 0; i < _segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * _config.GetDistance;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * _config.GetDistance;

            _lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += 360f / _segments;
        }
    }




    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.TryGetComponent(out UnitComponent unit))
        {

            if (unit.GetTypeUnit == TypeUnit.ENEMY)
            {


                IHealth enemy = unit.GetComponent<IHealth>();

                if (enemy != null && !_enemies.Contains(unit) && !enemy.IsDead)
                {
                    _enemies.Add(unit);

                    SelectEnemyForAttack();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out UnitComponent unit))
        {
            IHealth enemy = unit.GetComponent<IHealth>();
            if (enemy != null && _enemies.Contains(unit))
            {
                _enemies.Remove(unit);


                if (GetTargetForAttack == enemy)
                {
                    GetTargetForAttack = null;

                }
            }
            if (_enemies.Count > 0)
            {

                GetTargetForAttack = _enemies[0].GetComponent<IHealth>();
            }
            else
            {
                GetTargetForAttack = null;
            }
        }
    }
    //TODO=> Переделать IHealth на UnityComponent
    private void SelectEnemyForAttack()
    {
        if (_enemies.Count > 0)
        {
            foreach (UnitComponent enemy in _enemies)
            {
                // Проверка на уязвимость противника  
                if (enemy != null && (!enemy.GetComponent<IHealth>().IsDead || GetTargetForAttack == null))
                {
                    GetTarget = enemy.transform;
                    GetTargetForAttack = enemy.GetComponent<IHealth>();
                    break;
                }
            }

            SetState(AttackState);

        }

        else
        {
            GetTarget = null;
        }


    }
}
