
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Обновление событий для юнитов 
/// </summary>
public sealed class UnitsUpdateEngine : MonoBehaviour
{
    [SerializeField, Tooltip("Время для обнавления статусов")]
    private float _timerUpdate = 0.5f;
    private List<UnitComponent> _moveStateUnits = new();
    private List<UnitComponent> _otherStateUnits = new();
    private List<UnitComponent> _attackUpdate = new();

    //TODO => возможно . на удаление закрытых списков 
    public IReadOnlyList<UnitComponent> GetUnitsMove => _moveStateUnits;
    public IReadOnlyList<UnitComponent> GetUnitsOther => _otherStateUnits;
    //TODO => возможно . на удаление закрытых списков 



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
                if (_moveStateUnits.Contains(unit))
                {
                    _moveStateUnits.Remove(unit);
                }
                break;
            case StateUnitList.OTHER:

                if (_otherStateUnits.Contains(unit))
                {
                    _otherStateUnits.Remove(unit);
                }


                break;
                case StateUnitList.ATTACK:
                _attackUpdate.Remove(unit);
                break;
        }




    }

    private IEnumerator UpdateOtherStates()
    {
        while (true)
        {
            for (int i = _otherStateUnits.Count - 1; i >= 0; i--)
              
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

    private void OnEnable()
    {
        StartCoroutine(UpdateOtherStates());
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateOtherStates());
    }


    private void Update()
    {
        if (_moveStateUnits.Count <= 0)
        {
            return;
        }
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