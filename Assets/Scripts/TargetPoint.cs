using UnityEditor;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField, Range(0.0f, 2.0f)]
    private float _radius = 0.4f;
    public float AnglePoint;
    void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.up, AnglePoint.ToString());
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, _radius);
    }
#endif
}
