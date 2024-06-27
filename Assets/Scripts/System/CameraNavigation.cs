using System.Collections;
 
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraNavigation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float _speedMove = 1f;
    [SerializeField] private Border _activeBorder;
    [SerializeField] private bool _isMove = false;


    private void Update()
    {
        if (_isMove && _activeBorder != null)
        {
            Vector3 direction = GetDirection(_activeBorder);
            _camera.transform.position += direction * _speedMove * Time.deltaTime;
        }
    }

    private void Start()
    {

        _camera = Camera.main;
    }
    private Vector3 GetDirection(Border border)
    {
        // Пример логики направления движения в зависимости от границы
        switch (border.GetBorderType)
        {
            case BorderType.LEFT:
                return Vector3.left;

            case BorderType.RIGHT:
                return Vector3.right;

            case BorderType.TOP:
                return Vector3.up;

            case BorderType.BOTTOM:
                return Vector3.down;

            case BorderType.ANGLETR:
                return new Vector3(1, 1, 0);

            case BorderType.ANGLETL:
                return new Vector3(-1, 1, 0);
            case BorderType.ANGLEBR:
                return new Vector3(1, -1, 0);
            case BorderType.ANGLEBL:
                return new Vector3(-1, -1, 0);
            default:
                return Vector3.zero;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject hoveredObject = eventData.pointerEnter;

        if (hoveredObject != null)
        {
            if (hoveredObject.TryGetComponent<Border>(out var border))
            {
                _activeBorder = border;
                _isMove = true;
            }
           
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
         
           
            
                _isMove = false;
                _activeBorder = null;
         
       




    }
}
