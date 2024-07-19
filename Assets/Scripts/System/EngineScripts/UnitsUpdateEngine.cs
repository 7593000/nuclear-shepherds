using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обновление событий для юнитов 
/// </summary>
public sealed class UnitsUpdateEngine : MonoBehaviour
{
    [SerializeField, Tooltip("Время для обновления статусов")]
    private float _timerUpdate = 1f;
    [SerializeField, Tooltip("Время для обновления статусов, анимация")]
    private float _timerUpdateDirect = 1f;
    [SerializeField, Tooltip("Минимальное время задержки для атаки")]
    private float _minAttackDelay = 0.1f;

 

 
    private List<UnitComponent> _moveStateUnits = new();
    private List<UnitComponent> _otherStateUnits = new();
    private List<UnitComponent> _attackUpdate = new();
    private List<UnitComponent> _followGoal = new();
    private List<UnitComponent> _distanceForSuond = new();

    //TODO => возможно на удаление закрытых списков 
    public IReadOnlyList<UnitComponent> GetUnitsMove => _moveStateUnits;
    public IReadOnlyList<UnitComponent> GetUnitsOther => _otherStateUnits;
    public IReadOnlyList<UnitComponent> GetFollowGoal => _followGoal;

    /// <summary>
    /// Добавить юнит в лист состояния 
    /// </summary>
    public void AddUnit(UnitComponent unit, StateUnitList list)
    {
        switch (list)
        {
            case StateUnitList.MOVE:
                _moveStateUnits.Add(unit);
                break;
            case StateUnitList.OTHER:
                _otherStateUnits.Add(unit);
                break;
            case StateUnitList.ATTACK:
                _attackUpdate.Add(unit);
                break;
            case StateUnitList.DIRECT:
                _followGoal.Add(unit);
                break;
        }
    }

    /// <summary>
    /// Удалить юнит из листа состояния
    /// </summary>
    public void RemoveUnit(UnitComponent unit, StateUnitList list)
    {
        switch (list)
        {
            case StateUnitList.MOVE:
                _moveStateUnits.Remove(unit);
                break;
            case StateUnitList.OTHER:
                _otherStateUnits.Remove(unit);
                break;
            case StateUnitList.ATTACK:
                _attackUpdate.Remove(unit);
                break;
            case StateUnitList.DIRECT:
                _followGoal.Remove(unit);
                break;
        }
    }

    private IEnumerator FollowGoal()
    {
        while (true)
        {
            for (int i = 0; i < _followGoal.Count; i++)
            {
                _followGoal[i].StartAnimation.ChangeDirection();
            }
            yield return new WaitForSeconds(_timerUpdateDirect);  
        }
    }

    private IEnumerator UpdateOtherStates()
    {
        while (true)
        {
            for (int i = 0; i < _otherStateUnits.Count; i++)
            {
                if (_otherStateUnits[i].gameObject.activeSelf)
                {
                    _otherStateUnits[i].UpdateUnit();
                }
                else
                {
                    RemoveUnit(_otherStateUnits[i], StateUnitList.OTHER);
                }
            }
            yield return new WaitForSeconds(_timerUpdate);
        }
    }

    private IEnumerator UpdateAttackStates()
    {
        while (true)
        {
            for (int i = 0; i < _attackUpdate.Count; i++)
            {
                if (_attackUpdate[i].gameObject.activeSelf)
                {
                    _attackUpdate[i].UpdateUnit();
                    
                }
                else
                {
                    RemoveUnit(_attackUpdate[i], StateUnitList.ATTACK);
                }
            }
            yield return new WaitForSeconds(_minAttackDelay); // Минимальная задержка для атаки
        }
    }
    
    private void OnEnable()
    {
        StartCoroutine(UpdateOtherStates());
        StartCoroutine(FollowGoal());
        StartCoroutine(UpdateAttackStates());
     
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateOtherStates());
        StopCoroutine(FollowGoal());
        StopCoroutine(UpdateAttackStates());
     }

    private void Update()
    {
        if (_moveStateUnits.Count > 0)
        {
            for (int i = _moveStateUnits.Count - 1; i >= 0; i--)
            {
                if (_moveStateUnits[i].gameObject.activeSelf)
                {
                    _moveStateUnits[i].UpdateUnit();

                }
                else
                {
                    RemoveUnit(_moveStateUnits[i], StateUnitList.MOVE);
                }
            }
        }
    }
}
