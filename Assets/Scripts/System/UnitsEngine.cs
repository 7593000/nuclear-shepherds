
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UnitsEngine : MonoBehaviour
{
    [SerializeField, Tooltip( "Время для обнавления статусов" )]
    private float _timerUpdate = 1f;
    private List<UnitComponent> _moveStateUnits = new();
    private List<UnitComponent> _otherStateUnits = new();

    //TODO => возможно . на удаление закрытых списков 
    public IReadOnlyList<UnitComponent> GetUnitsMove => _moveStateUnits;
    public IReadOnlyList<UnitComponent> GetUnitsOther => _otherStateUnits;
    /// <summary>
    /// Добавить юнит в лист состояния 
    /// </summary>

    public void AddUnit( UnitComponent unit , StateUnitList list )
    {
        switch ( list )
        {
            case StateUnitList.MOVE:
                _moveStateUnits.Add( unit );
                break;
            case StateUnitList.OTHER:
                _otherStateUnits.Add( unit );

                break;
        }
    }

    /// <summary>
    /// Удалить юнит из листа состояния
    /// </summary>

    public void RemoveUnit( UnitComponent unit , StateUnitList list )
    {
        switch ( list )
        {
            case StateUnitList.MOVE:
                if ( _moveStateUnits.Contains( unit ) )
                {
                    _moveStateUnits.Remove( unit );
                }
                break;
            case StateUnitList.OTHER:

                if ( _otherStateUnits.Contains( unit ) )
                {
                    _otherStateUnits.Remove( unit );
                }


                break;
        }




    }

    private IEnumerator UpdateOtherStates()
    {
        while ( true )
        {

            foreach ( UnitComponent unit in _otherStateUnits )
            {
                if ( unit.gameObject.activeSelf )
                {
                    unit.UpdateUnit();
                }
                else
                {
                    RemoveUnit( unit , StateUnitList.OTHER );
                }
            }
            yield return new WaitForSeconds( _timerUpdate );
        }
    }

    private void OnEnable()
    {
        StartCoroutine( UpdateOtherStates() );
    }

    private void OnDisable()
    {
        StopCoroutine( UpdateOtherStates() );
    }


    private void Update()
    {
        if ( _moveStateUnits.Count <= 0 )
        {
            return;
        }

        foreach ( UnitComponent unit in _moveStateUnits )
        {
            if ( unit.gameObject.activeSelf )
            {
                unit.UpdateUnit();
            }
            else
            {
                RemoveUnit( unit , StateUnitList.MOVE );
            }
        }
    }

}