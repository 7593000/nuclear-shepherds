using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadGameWindow : MonoBehaviour
{

    private CanvasGroup _group;
    private bool _visible = false;
    [SerializeField] private LoadGameItem _itemPrefab;
    [SerializeField] private Transform _parent;
    private List<LoadGameItem> _dataItems = new();

    [SerializeField] private TMP_Text _h;

    public void Initialized(string[] save)
    {

        if (save.Length > _dataItems.Count)
        {
            int count = save.Length - _dataItems.Count;

            for (int i = 0; i < count; i++)
            {
                LoadGameItem item = Instantiate(_itemPrefab, _parent);
                item.Initialized();
                item.ShowHideItems(false);
                _dataItems.Add(item);
            }
        }

        for (int i = 0; i < save.Length; i++)
        {
            _dataItems[i].ItemStatus(save[i]);
            _dataItems[i].ShowHideItems(true);
            _dataItems[i].gameObject.SetActive(true);
        }


        for (int i = save.Length; i < _dataItems.Count; i++)
        {

            _dataItems[i].ShowHideItems(false);
            _dataItems[i].gameObject.SetActive(false);
        }



    }




    /// <summary>   
    /// Активировать или скрыть меню загрузки
    /// </summary>
    public void LoadWindowStatus()
    {

        _visible = !_visible;
        _group.alpha = _visible ? 1 : 0;
        _group.blocksRaycasts = _visible;
        _group.interactable = _visible;
    }


    public void PrintAllSaveFiles(string[] pathSave)
    {


        for (int i = 0; i < pathSave.Length; i++)
        {
            _dataItems[i].ItemStatus(pathSave[i]);

        }


    }



    public void BackToMenu()
    {


        _visible = !_visible;
        _group.alpha = _visible ? 1 : 0;
        _group.blocksRaycasts = _visible;
        _group.interactable = _visible;
    }
    private void Start()
    {
        _h.color = GameMenu.H1;
        _h.fontStyle = GameMenu.FONTSTYLEH1;
        _h.SetAllDirty();
        _group = GetComponent<CanvasGroup>();
        _group.blocksRaycasts = false;
        _group.interactable = false;
        _group.alpha = 0f;
    }


}
