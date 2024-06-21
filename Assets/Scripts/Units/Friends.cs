using System.Collections.Generic;
using UnityEngine;

public class Friends : UnitComponent, IAttack

{
    private LineRenderer _lineRenderer;
    private CircleCollider2D _circleCollider;
    private int _segments = 46;
    private List<IHealth> _enemies = new List<IHealth>();
 
    
    
    public void Attack()
    {

        GetAttack.AttackTarget(GetTargetForAttack);
       
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
        Debug.Log("1");

        if (other.TryGetComponent<UnitComponent>(out UnitComponent unit))
        {
           
            if (unit.GetTypeUnit == TypeUnit.ENEMY)
            {
               

                IHealth enemy = unit.GetComponent<IHealth>();
                if (enemy != null && !_enemies.Contains(enemy) && !enemy.IsDead)
                {
                    _enemies.Add(enemy);
                    GetTarget = unit.transform;
                    SelectEnemyForAttack();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<UnitComponent>(out UnitComponent unit))
        {
            IHealth enemy = unit.GetComponent<IHealth>();
            if (enemy != null && _enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
               

                if (GetTargetForAttack == enemy)
                {
                    GetTargetForAttack = null;
                   
                }
            }
            if (_enemies.Count > 0)
            {
                GetTargetForAttack = _enemies[0];
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
            foreach (IHealth enemy in _enemies)
            {
                // Проверка на уязвимость противника  
                if (enemy != null && (!enemy.IsDead || GetTargetForAttack == null))
                {
                    GetTargetForAttack = enemy;
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
