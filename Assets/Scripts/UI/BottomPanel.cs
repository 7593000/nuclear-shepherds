using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

#if UNITY_EDITOR
#endif
public class BottomPanel : MonoBehaviour, IPointerClickHandler,  IBeginDragHandler, IDragHandler  , IEndDragHandler
{
    private GameHub _gameHub;
    [SerializeField]
    private List<UnitConfig> _unitConfigs;
    [SerializeField]
    private ShopWindow _shopWindow;
    [SerializeField]
    private ScreenPanel _screenPanel;
    [SerializeField]
    private string _formatText = "����: {0}\n������: {1}\n����: {2}\n�����: {3}";

    private string _textInfo;
    private List<CardUnit> cardUnits = new();
        private Canvas _canvas;
    private Vector3 _startPositionCard;
    private Transform _draggedTransform;
    private GameObject _dragShadow;
    private CardUnit _activeCard = null;
    public GameObject _shadowPrefab;

    public void Initialized(GameHub gameHub )
    {
        _gameHub = gameHub;
        
        _canvas = GetComponentInParent<Canvas>();
        _shopWindow ??= FindFirstObjectByType<ShopWindow>();
        _screenPanel ??= FindFirstObjectByType<ScreenPanel>();
        _dragShadow = Instantiate( _shadowPrefab );
        _dragShadow.SetActive( false );  
      
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
    

    public void OnBeginDrag( PointerEventData eventData )
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if ( clickedObject != null )
        {
            CardUnit cardUnit = clickedObject.GetComponent<CardUnit>();
            if ( cardUnit != null )
            {
                _activeCard = cardUnit;
                // ������������� ������ ����

                RectTransform shadowRectTransform = _dragShadow.GetComponent<RectTransform>();
                Vector2 spriteOriginSize = cardUnit.GetSprite.rect.size * 2;//Todo=> �������� 
                shadowRectTransform.sizeDelta = spriteOriginSize;
                _dragShadow.GetComponent<Image>().sprite = cardUnit.GetSprite;
              
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
            
       //TODO => �������� �������. �������� �� ���������� ���� �� �����. ���� ���

            if (  _gameHub.GetTileMap.CheckedCell() )
            {
                
                Debug.Log( "���������� ����" );
                UnitComponent unit= Instantiate( _activeCard.GetConfig.GetPrefab );
                unit.transform.position = _dragShadow.transform.position;
                _dragShadow.SetActive( false );
                _dragShadow.GetComponent<Image>().sprite = null;
            }


        }
        _activeCard = null;
    }

    private void UpdateDragShadowPosition( PointerEventData eventData )
    {
        if ( _canvas != null )
         {
        //    RectTransformUtility.ScreenPointToWorldPointInRectangle(
        //        _canvas.transform as RectTransform ,
        //        eventData.position ,
        //        eventData.pressEventCamera ,
        //        out Vector3 globalPosition
        //    );
            
          
            
                _dragShadow.transform.position = _gameHub.GetTileMap.GetPositionCell();

             
          
         //   _dragShadow.transform.position = globalPosition;
        }
    }



    #region EDITOR
    #endregion
}

