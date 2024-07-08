using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadGameWindow : MonoBehaviour, IPointerClickHandler
{
    private GameSettings _gameSettings;
    private CanvasGroup _group;
    private bool _visible = false;
    [SerializeField] private LoadGameItem _itemPrefab;
    [SerializeField] private Transform _parent;
    private List<LoadGameItem> _dataItems = new();
    private string[] _items = null;

    public void Initialized( GameSettings settings )
    {
        _dataItems.Clear();
        _gameSettings = settings;
        int items = _gameSettings.GetMaxSaveGame;
        for ( int i = 0; i < items; i++ )
        {
            LoadGameItem item = Instantiate( _itemPrefab , _parent );
            item.Initialized();

            _dataItems.Add( item );

        }

 

    }

    private void CountItems( string loadData )
    {
        if ( loadData == null )
        { return; }
        _items = loadData.Split( new char[] { ',' } , System.StringSplitOptions.RemoveEmptyEntries );

        for ( int i = 0; i < _items.Length; i++ )
        {
            _dataItems[ i ].ItemStatus( _items[ i ] );

        }


    }


    /// <summary>   
    /// Активировать или скрыть меню загрузки
    /// </summary>
    public void LoadWindowStatus( string path )
    {

        CountItems( path );


        _visible = !_visible;
        _group.alpha = _visible ? 1 : 0;
        _group.blocksRaycasts = _visible;
        _group.interactable = _visible;
    }
    public void BackToMenu()
    {
        for ( int i = 0; i < _items.Length; i++ )
        {
            _dataItems[ i ].ItemStatus( _items[ i ] );

        }


        _visible = !_visible;
        _group.alpha = _visible ? 1 : 0;
        _group.blocksRaycasts = _visible;
        _group.interactable = _visible;
    }
    private void Start()
    {
        _group = GetComponent<CanvasGroup>();
        _group.blocksRaycasts = false;
        _group.interactable = false;
        _group.alpha = 0f;
    }

    public void OnPointerClick( PointerEventData eventData )
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if ( clickedObject != null )
        {

            LoadGameItem itemData = clickedObject.GetComponent<LoadGameItem>();
            if ( itemData != null )
            {
                _gameSettings.LoadGame( itemData.GetPath );
            }
        }
    }
}
