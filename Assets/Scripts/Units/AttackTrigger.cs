using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private UnitComponent _unit;

    private CircleCollider2D _collider;
 
    private List<UnitComponent> _enemies = new();




    public void Initialized(UnitComponent unit)
    {
        _unit = unit;
        _collider = GetComponent<CircleCollider2D>();
        _collider.radius = _unit.GetDistance;
       
    }


    //TODO=> проверка нахождения кого-то в триггере .. 
    // не срабатывает триггер если разместить юнита рядом с врагом 
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


                if (_unit.GetTargetForAttack == enemy)
                {
                    _unit.GetTargetForAttack = null;
                    _unit.GetTarget = null;
                }


                if (_enemies.Count > 0)
                {
                    SelectEnemyForAttack();
                }
            }
        }
    }

    private void SelectEnemyForAttack()
    {
        if (_enemies.Count > 0)
        {
            foreach (UnitComponent enemy in _enemies)
            {
                IHealth enemyHealth = enemy.GetComponent<IHealth>();
                if (enemyHealth != null && !enemyHealth.IsDead)
                {
                    _unit.GetTarget = enemy.transform;
                    _unit.GetTargetForAttack = enemyHealth;
                    break;
                }
            }


            if (_unit.GetTarget != null)
            {
                _unit.SetState(_unit.AttackState);
            }
        }
        else
        {
            _unit.GetTarget = null;
            _unit.GetTargetForAttack = null;
        }
    }

     
}
