using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class PointsTargerEngine : MonoBehaviour
{


    [SerializeField]
    private List<TargetPoint> _target = new();

    public IReadOnlyList<TargetPoint> GetTargets => _target;


#if UNITY_EDITOR
    public string _nameGameObjectPoints = "TargetPoint";

    [ContextMenu("Rename points")]
    private void RenameFiles()
    {
        int index = 0;
        foreach (var target in _target)
        {
            target.gameObject.name = $"{_nameGameObjectPoints}-{index}";

            index++;

        }
    }

  

        [ContextMenu("Install the brakes")]
    private void InstallBrakes()
    {
        CheckingAngle();


    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _target.Count; i++)
        {
            var nextIndex = i + 1;
            if (nextIndex < _target.Count)
            {
                var nextTarget = _target[nextIndex];
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(_target[i].transform.position, nextTarget.transform.position);
            }
        }
    }

    private void CheckingAngle()
    {
        for ( int i = 0; i < _target.Count - 2; i++ )
        {
            Vector3 pointA = _target[ i ].transform.position;
            Vector3 pointB = _target[ i + 1 ].transform.position;
            Vector3 pointC = _target[ i + 2 ].transform.position;

            Vector3 vectorAB = ( pointB - pointA ).normalized;
            Vector3 vectorBC = ( pointC - pointB ).normalized;

            

            float angleFromAtoB = Mathf.Atan2( vectorAB.y , vectorAB.x ) * Mathf.Rad2Deg;
            if ( angleFromAtoB < 0 )
            {
                angleFromAtoB += 360;
            }

#if UNITY_EDITOR
            _target[ i + 1 ].AnglePoint = angleFromAtoB;
#endif
            

            if ( _target[ i + 1 ] == null )
                return;
        }
    }
#endif

}
