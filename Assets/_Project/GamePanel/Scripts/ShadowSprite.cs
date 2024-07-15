using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    [SerializeField] private Canvas _canvas; // Canvas компонент
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _segments = 46;

    private void Awake()
    {
        // Проверяем и инициализируем LineRenderer
        _lineRenderer ??= GetComponent<LineRenderer>();

    }
    public void Initialize(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void CreateCircle(float radius, int segments = 46)
    {

        if (_lineRenderer == null)
        {
            Debug.LogError("LineRenderer не добавлен");
            return;
        }
        _lineRenderer.positionCount = segments + 1;
        _lineRenderer.useWorldSpace = false;
        Vector3 canvasScale = _canvas.transform.localScale;
        float scaledRadius = radius / canvasScale.x;

        float angle = 0f;
        for (int i = 0; i < _segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * scaledRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * scaledRadius;

            _lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += 360f / _segments;
        }
    }


}