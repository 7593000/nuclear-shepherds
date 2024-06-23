using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

#if UNITY_EDITOR
#endif
public class BottomPanel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler 
{
    [SerializeField]
    private List<UnitConfig> _unitConfigs;
    [SerializeField]
    private ShopWindow _shopWindow;
    [SerializeField]
    private ScreenPanel _screenPanel;
    [SerializeField]
    private string _formatText = "Юнит: {0}\nОружие: {1}\nУрон: {2}\nУдача: {3}";

    private string _textInfo;
    private List<CardUnit> cardUnits = new();
        private Canvas _canvas;
    private Vector3 _startPositionCard;
    private Transform _draggedTransform;
    private GameObject _dragShadow;

    public GameObject _shadowPrefab;

    public void Initialized()
    {
        _shopWindow ??= FindFirstObjectByType<ShopWindow>();
        _screenPanel ??= FindFirstObjectByType<ScreenPanel>();
        _canvas = GetComponentInParent<Canvas>();

        _dragShadow = Instantiate( _shadowPrefab );
        _dragShadow.SetActive( false ); // Скрываем тень по умолчанию
      
        if ( _canvas != null )
        {
            _dragShadow.transform.SetParent( _canvas.transform , false );
        }
        CreateCards();
       
     
    }

    private void CreateCards()
    {
        foreach ( UnitConfig unitConfig in _unitConfigs )
        {
            _shopWindow.AddUnitsForSell( unitConfig );
        }
    }
    public void OnPointerClick( PointerEventData eventData )
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if ( clickedObject != null )
        {
            
            CardUnit cardUnit = clickedObject.GetComponent<CardUnit>();
            if ( cardUnit != null )
            {
                _textInfo = string.Format( _formatText , cardUnit.GetName , cardUnit.GetTypeWeapon , cardUnit.GetDamage, cardUnit.GetLuch);
            
                _screenPanel.ShowText( _textInfo );
            }
            
        }
        
    }
    public void OnPointerEnter( PointerEventData eventData )
    {
        Debug.Log( "Move" );
    }
    public void OnPointerExit( PointerEventData eventData )
    {
        
    }

    public void OnBeginDrag( PointerEventData eventData )
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if ( clickedObject != null )
        {
            CardUnit cardUnit = clickedObject.GetComponent<CardUnit>();
            if ( cardUnit != null )
            {
                // Устанавливаем спрайт тени
                _dragShadow.GetComponent<Image>().sprite  = cardUnit.GetSprite;
                _dragShadow.GetComponent<Image>().SetNativeSize();
                // Обновляем позицию тени
                UpdateDragShadowPosition( eventData );
                _dragShadow.SetActive( true ); 
            }
        }
    }

    public void OnDrag( PointerEventData eventData )
    {
        if ( _dragShadow != null )
        {
            UpdateDragShadowPosition( eventData );
        }
    }

    public void OnEndDrag( PointerEventData eventData )
    {
        if ( _dragShadow != null )
        {
            _dragShadow.SetActive( false );  
       //TODO => проверка позиции. возможно ли разместить юнит на карте. если нет
        
        }
    }

    private void UpdateDragShadowPosition( PointerEventData eventData )
    {
        if ( _canvas != null )
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                _canvas.transform as RectTransform ,
                eventData.position ,
                eventData.pressEventCamera ,
                out Vector3 globalPosition
            );
            _dragShadow.transform.position = globalPosition;
        }
    }



    #region EDITOR
    #endregion
}

