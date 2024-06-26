using UnityEngine;
 
public class ShadowSprite : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _segments = 46;

    private void Awake()
    {
        // Проверяем и инициализируем LineRenderer
        _lineRenderer ??= GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = false;
    }

    public void CreateCircle(float radius, int segments = 46)
    {
        _segments = segments;
        _lineRenderer.positionCount = _segments + 1;

        float angleIncrement = 360f / _segments;
        float angle = 0f;

        for (int i = 0; i <= _segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            _lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
            angle += angleIncrement;
        }
    }
}
