using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
#endif
public class BottomPanel : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameHub _gameHub;
    [SerializeField]
    private List<UnitConfig> _unitConfigs;
    [SerializeField]
    private ShopWindow _shopWindow;
    [SerializeField]
    private ScreenPanel _screenPanel;
    [SerializeField]
    private string _formatText = "Юнит: {0}\nОружие: {1}\nУрон: {2}\nУдача: {3}";

    private string _textInfo;
    private Canvas _canvas;
    public GameObject _shadowPrefab;
    private GameObject _dragShadow;
    private CardUnit _activeCard = null;
    private bool _tilemapStatus = false;    
    private Vector3 _positionForUnit;
  
    
    public void Initialized(GameHub gameHub)
    {
        _gameHub = gameHub;

        _canvas = GetComponentInParent<Canvas>();
        _shopWindow ??= FindFirstObjectByType<ShopWindow>();
        _screenPanel ??= FindFirstObjectByType<ScreenPanel>();
        _dragShadow = Instantiate(_shadowPrefab);
        _dragShadow.SetActive(false);

        if (_canvas != null)
        {
            _dragShadow.transform.SetParent(_canvas.transform, false);
        }
        CreateCards();


    }

    private void CreateCards()
    {
        foreach (UnitConfig unitConfig in _unitConfigs)
        {
            _shopWindow.AddUnitsForSell(unitConfig);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if (clickedObject != null)
        {

            CardUnit cardUnit = clickedObject.GetComponent<CardUnit>();
            if (cardUnit != null)
            {
                _textInfo = string.Format(_formatText, cardUnit.GetName, cardUnit.GetTypeWeapon, cardUnit.GetDamage, cardUnit.GetLuch);

                _screenPanel.ShowText(_textInfo);
            }

        }

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if (clickedObject != null)
        {
            CardUnit cardUnit = clickedObject.GetComponent<CardUnit>();
            if (cardUnit != null)
            {
                _activeCard = cardUnit;
                if(_tilemapStatus == false)
                {
                    _tilemapStatus = true;
                    _gameHub.GetTileMap.TileMapActivity(_tilemapStatus);
                }

                // Устанавливаем спрайт тени
                RectTransform shadowRectTransform = _dragShadow.GetComponent<RectTransform>();
                Vector2 spriteOriginSize = cardUnit.GetSprite.rect.size * 2;//Todo=> времянка 
                shadowRectTransform.sizeDelta = spriteOriginSize;
                _dragShadow.GetComponent<Image>().sprite = cardUnit.GetSprite;
                _dragShadow.transform.position = transform.position;
              
                _dragShadow.SetActive(true);
                _dragShadow.GetComponent<ShadowSprite>().CreateCircle(cardUnit.GetDistance);
                UpdateDragShadowPosition(eventData);
               
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_dragShadow != null)
        {
            UpdateDragShadowPosition(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
         if (_dragShadow != null)
            {
                if (_gameHub.GetTileMap.CheckedCell())
                {
                    Debug.Log("Установлен юнит");

                    UnitComponent unit = Instantiate(_activeCard.GetConfig.GetPrefab);
                   
                if (unit != null)
                    {
                        Vector3Int cellPosition = _gameHub.GetTileMap._tilemap.WorldToCell(_dragShadow.transform.position);
                        unit.transform.position = _dragShadow.transform.position;
                        _dragShadow.SetActive(false);
                        
                        _dragShadow.transform.position = transform.position;
                        _gameHub.GetTileMap.AddCell(cellPosition);
                  
                    if (_tilemapStatus == true)
                    {
                        _tilemapStatus = false;
                        _gameHub.GetTileMap.TileMapActivity(_tilemapStatus);
                    }
                }
                    else
                    {
                        Debug.LogError("С юнитом беда");
                    }
                }
            }
            _activeCard = null;
         
    }

    private void UpdateDragShadowPosition(PointerEventData eventData)
    {
        if (_canvas != null)
        {
            _positionForUnit = _gameHub.GetTileMap.GetPositionCell();
            _dragShadow.transform.position = _positionForUnit;
        }
    }



    
}

