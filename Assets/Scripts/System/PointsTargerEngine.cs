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
         

        for (int i = 0; i < _target.Count - 2; i++)
        {
            Vector3 VectorAB = (_target[i].transform.position - _target[i + 1].transform.position).normalized;
            Vector3 VectorBC = (_target[i + 1].transform.position - _target[i + 2].transform.position).normalized;

            float dotProduct = Vector3.Dot(VectorAB, VectorBC);
            float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
#if UNITY_EDITOR 
            _target[i + 1].AnglePoint = angle;
#endif
            if (_target[i + 1] == null) return;
           
        }

    }
#endif

}
