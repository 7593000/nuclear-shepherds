using UnityEditor;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{


    [Header("Координаты для анимации [-1, 0 , 1]")]
    public int[] _anglePosition = new int[2];
    /// <summary>
    /// Получить координаты для анимации ( -1, 0 , 1 ) 
    /// </summary>
    public int[] GetAngleForanimation => _anglePosition;


#if UNITY_EDITOR
    public Sprite _spriteHelp;
    public Sprite GetSprite => _spriteHelp;

    [SerializeField, Range(0.0f, 2.0f)]
    private float _radius = 0.4f;
    public float AnglePoint { get; set; }



    void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.up, AnglePoint.ToString());
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, _radius);
    }
#endif
}
