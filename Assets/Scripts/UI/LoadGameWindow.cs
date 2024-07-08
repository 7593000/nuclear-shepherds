using System.Collections.Generic;
using UnityEngine;

public class LoadGameWindow : MonoBehaviour
{
    private CanvasGroup _group;
    private bool _visible = false;
    [SerializeField] private LoadGameItem _itemPrefab;
    [SerializeField] private Transform _parent;
    private List<LoadGameItem> _dataItems = new();
    private string[] _items = null;

    public void Initialized(int items)
    {
        _dataItems.Clear();
        //_items = loadData.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
       
         for(int i = 0; i < items; i++) {
            LoadGameItem item = Instantiate(_itemPrefab, _parent);
             item.Initialized();
           // item.enabled = false;
            _dataItems.Add(item);

        }


        //foreach (string itemText in _items)
        //{
        //    LoadGameItem item = Instantiate(_itemPrefab, _parent);
        //    item.Initialized(itemText);
        //    _dataItems.Add(item);
        //}
      
    }

    private void CountItems(string loadData)
    {
        if(loadData ==null) { return; }
        _items = loadData.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 0; i < _items.Length; i++)
        {
            _dataItems[i].ItemStatus(_items[i]);

        }

            
    }
 

    /// <summary>   
    /// Активировать или скрыть меню загрузки
    /// </summary>
    public void LoadWindowStatus(string path)
    {

        CountItems(path);
         

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
}
