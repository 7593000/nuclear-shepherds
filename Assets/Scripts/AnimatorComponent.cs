using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class AnimatorComponent : MonoBehaviour
{
    private UnitComponent _unit;

    private int _positionX;
    private int _positionY;
    private Animator _animator;
    private StateUnit _currentStatus;
    private string _activeParameters;
    public void Container(UnitComponent unit)
    {
        _unit = unit;
        _animator = unit.GetAnimator;
    }

    public void ToRun(StateUnit state)
    { 
        if (_currentStatus == state) return;
      
        _currentStatus = state;
        switch (state)
        {

            case StateUnit.IDLE:
                Animation("Idle");
             
                break;
            case StateUnit.MOVE:
                Animation("Move");
                break;
            case StateUnit.ATTACK:

                Animation("Attack");
                break;
            case StateUnit.DEAD:
                Animation("Dead");
                break;
        }
       
    }

    private void Animation(string parameters )
    {
      if(_activeParameters == null) _activeParameters = parameters;

        _positionX = _unit.GetDirectionView[0] ;
        _positionY = _unit.GetDirectionView[1] ;

        _animator.ResetTrigger(_activeParameters);
       
        _animator.SetFloat("PositionX", _positionX);
        _animator.SetFloat("PositionY", _positionY);
        _animator.SetTrigger(parameters);
        _activeParameters = parameters;
    }

    public void ChangeDirection()
    {
        Vector3 pointA = _unit.transform.position;
        Vector3 pointB = _unit.GetTarget.position;
        

        Vector3 vectorAB = (pointB - pointA).normalized;
         

        float angleFromAtoB = Mathf.Atan2(vectorAB.y, vectorAB.x) * Mathf.Rad2Deg;
        (float _positionX, float _positionY) = Comparison((int)angleFromAtoB);
      
        _animator.SetFloat("PositionX", _positionX); 
        _animator.SetFloat("PositionY", _positionY); 

    }

    private (float, float) Comparison(int angle)
    {
       
        if (angle < 0)
        {
            angle += 360;
        } Debug.Log(angle);

        if (angle >= 0 && angle < 45)
            return (1, 0);
        else if (angle >= 45 && angle < 90)
            return (1, 1);
        else if (angle>=90 && angle < 157)
            return (-1, 1);
        else if (angle >= 157 && angle < 202)
            return (-1, 0);
        else if (angle >= 202 && angle < 270)
            return (-1, -1);
        else if (angle >= 270 && angle < 360)
            return (1, -1);
        



        return (0, 0);
    }



}
