
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���������� ������� ��� ������ 
/// </summary>
public sealed class UnitsUpdateEngine : MonoBehaviour
{
    [SerializeField, Tooltip("����� ��� ���������� ��������")]
    private float _timerUpdate = 0.5f;
    private List<UnitComponent> _moveStateUnits = new();
    private List<UnitComponent> _otherStateUnits = new();
    private List<UnitComponent> _attackUpdate = new();
    private List<UnitComponent> _followGoal = new();

    //TODO => �������� . �� �������� �������� ������� 
    public IReadOnlyList<UnitComponent> GetUnitsMove => _moveStateUnits;
    public IReadOnlyList<UnitComponent> GetUnitsOther => _otherStateUnits;
    public IReadOnlyList<UnitComponent> GetFollowGoal => _followGoal;
    //TODO => �������� . �� �������� �������� ������� 



    /// <summary>
    /// �������� ���� � ���� ��������� 
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
    /// ������� ���� �� ����� ���������
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
                if (_otherStateUnits.Contains(unit))
                {
                    _attackUpdate.Remove(unit);
                }
                break;
            case StateUnitList.DIRECT:
                if (_otherStateUnits.Contains(unit))
                {
                    _followGoal.Remove(unit);
                }
                break;
        }
    }

    private IEnumerator FollowGoal()
    {
        while (true)
        {
            for (int i = 0; i < _followGoal.Count; i++)
            {
                //  _followGoal[i].GetDirectionView = _followGoal[i].GetTargetForAttack;
                _followGoal[i].StartAnimation.ChangeDirection();
            }


            yield return new WaitForSeconds(_timerUpdate); //TODO => � ����������

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

    private void OnEnable()
    {
        StartCoroutine(UpdateOtherStates());
        StartCoroutine(FollowGoal());
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateOtherStates());
        StopCoroutine(FollowGoal());
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