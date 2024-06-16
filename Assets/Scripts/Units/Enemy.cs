using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent( typeof(Rigidbody2D ) )]
public class Enemy : UnitComponent
{
    private Rigidbody2D _rb;
    /// <summary>
    /// ��������� ���� ��� �������� : �������� � ����; ����� ����;
    /// </summary>
    public Transform GetSelectedGoal;

    public override void Move()
    {
        Vector2 direction = transform.up;

         
        Vector2 newPosition = _rb.position + direction * _config.GetSpeed * Time.fixedDeltaTime;

        
        _rb.MovePosition( newPosition );
    }

    private void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();

     //   SetState( MoveState );
    }
}
