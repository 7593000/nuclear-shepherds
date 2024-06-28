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
    private List<UnitConfig> _unitConfigs = new();
    [SerializeField]
    private ShopWindow _shopWindow;
    [Space]
    [Header("Текстовые элементы на панеле")]
    [SerializeField]
    private TextPanel _screenPanel;
    [SerializeField]
    private TextPanel _walletPanel;
    [SerializeField]
    private TextPanel _brahminCountText;
    [SerializeField]
    private TextPanel _wavelCountText;

    private WaveEngine _waveEngine;
    private BrahminManager _brahminManager;
    private Wallet _wallet;
    [SerializeField]
    private string _formatTextForScreenPanel = "Юнит: {0}\nОружие: {1}\nУрон: {2}\nУдача: {3}";

    private string _textInfo;
    private Canvas _canvas;
    public ShadowSprite _shadowPrefab;
    private ShadowSprite _dragShadow;
    private CardUnit _activeCard = null;
    private bool _tilemapStatus = false;
    [SerializeField] private bool _transfer = false;
    private Vector3 _positionForUnit;


    public void Initialized(GameHub gameHub)
    {
        _gameHub = gameHub;
        _canvas = GetComponentInParent<Canvas>();


        _wallet = gameHub.GetWalletEngine.GetWallet;
        _brahminManager = gameHub.GetBrahmin;
        _waveEngine = gameHub.GetWaveEngine;


         _shopWindow ??= FindFirstObjectByType<ShopWindow>();
        _screenPanel ??= FindFirstObjectByType<ScreenPanel>();
        _dragShadow = Instantiate(_shadowPrefab);
        _dragShadow.Initialize(_canvas);
        _dragShadow.gameObject.SetActive(false);

        _wallet.OnCoinsChanged += (int value) => ChangingNumberCoins(value);
        _brahminManager.OnBrahmin += (int value) => ChangingText(_brahminCountText, value.ToString());
        _waveEngine.OnWave+=(int value) => ChangingText(_wavelCountText, value.ToString());
        ChangingText(_brahminCountText, _brahminManager.GetBrahminList.Count.ToString());

        if (_canvas != null)
        {
            _dragShadow.transform.SetParent(_canvas.transform, false);
        }

       
        CreateCards();
        ChangingNumberCoins(_wallet.Coins);

    }

    private void OnDestroy()
    {
        _wallet.OnCoinsChanged -= (int value) => ChangingNumberCoins(value);
        _brahminManager.OnBrahmin -= (int value) => ChangingText(_brahminCountText, value.ToString());
        _waveEngine.OnWave -= (int value) => ChangingText(_wavelCountText, value.ToString());
    }
  
    /// <summary>
    /// Изменять текст на панеле 
    /// </summary>
    /// <param name="panel">в какой панеле менять текст</param>
    /// <param name="value">Значение для изменения </param>
    private void ChangingText(TextPanel panel, string value)
    {
      
        panel.SetText(value);

    }


    private void ChangingNumberCoins( int value)
    {
        ChangingText(_walletPanel, value.ToString());
        _shopWindow.ChangingCoins(value);

    }

    private void CreateCards()
    {
        foreach ( UnitConfig unitConfig in _unitConfigs )
        {
            _shopWindow.AddUnitsForSell( unitConfig );
        }
    }

    #region DRAG AND DROP LOGIC
    public void OnPointerClick( PointerEventData eventData )
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if ( clickedObject != null )
        {

            CardUnit cardUnit = clickedObject.GetComponent<CardUnit>();
            if ( cardUnit != null )
            {
                _textInfo = string.Format( _formatTextForScreenPanel , cardUnit.GetName , cardUnit.GetTypeWeapon , cardUnit.GetDamage , cardUnit.GetLuch );

                ChangingText(_screenPanel, _textInfo);
                //_screenPanel.ShowText( _textInfo );
            }

        }

    }

    public void OnBeginDrag( PointerEventData eventData )
    {
        

        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if ( clickedObject != null )
        {
            CardUnit cardUnit = clickedObject.GetComponent<CardUnit>();
            
            if ( cardUnit != null && cardUnit.IsActive )
            {
                
                _transfer = true;
                _activeCard = cardUnit;

                if ( _tilemapStatus == false )
                {
                    _tilemapStatus = true;
                    _gameHub.GetTileMap.TileMapActivity( _tilemapStatus );
                }

                // Устанавливаем спрайт тени
                RectTransform shadowRectTransform = _dragShadow.GetComponent<RectTransform>();
                Vector2 spriteOriginSize = cardUnit.GetSprite.sprite.rect.size * 2;//Todo=> времянка 
                shadowRectTransform.sizeDelta = spriteOriginSize;
                _dragShadow.GetComponent<Image>().sprite = cardUnit.GetSprite.sprite;
                _dragShadow.CreateCircle(cardUnit.GetDistance);
                _dragShadow.transform.position = transform.position;

                _dragShadow.gameObject.SetActive( true );

                UpdateDragShadowPosition( eventData );

            }
        }
    }

    public void OnDrag( PointerEventData eventData )
    {
        if (!_transfer) return;

        if ( _dragShadow != null )
        {
            UpdateDragShadowPosition( eventData );
        }
    }

    public void OnEndDrag( PointerEventData eventData )
    {
        if (!_transfer) return;
        if ( _dragShadow != null)
        {
            if ( _gameHub.GetTileMap.CheckedCell() )
            {
                if (_wallet.TakeCurrency(_activeCard.GetPrice ))
                {
                    UnitComponent unit = Instantiate(_activeCard.GetConfig.GetPrefab);

                    if (unit != null)
                    {
                        Vector3Int cellPosition = _gameHub.GetTileMap._tilemap.WorldToCell(_dragShadow.transform.position);
                        unit.transform.position = _dragShadow.transform.position;

                        _gameHub.GetTileMap.AddCell(cellPosition);

                    }
                    else
                    {
                        Debug.LogError("С юнитом беда");
                    }
                }

         
            }
        }
        _tilemapStatus = false;
        _gameHub.GetTileMap.TileMapActivity( _tilemapStatus );
        _dragShadow.gameObject.SetActive( false );
        _dragShadow.transform.position = transform.position;
       
        _activeCard = null;
        _transfer = false;

    }

    private void UpdateDragShadowPosition( PointerEventData eventData )
    {
        if ( _canvas != null )
        {
            _positionForUnit = _gameHub.GetTileMap.GetPositionCell();
            _dragShadow.transform.position = _positionForUnit;
        }

    }

    #endregion


}

