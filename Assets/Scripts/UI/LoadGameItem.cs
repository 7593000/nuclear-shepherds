using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameItem : MonoBehaviour
{
   [SerializeField] private TextPanel _textPanel;

    private CanvasGroup _group;
    private bool _visible = false;
    private string _path;

    public void Initialized( )
    {

        _group = GetComponent<CanvasGroup>();



    }
    public void ItemStatus(string path)
    {
        _path = path;
        _textPanel.SetText(path);
        StatusitemWindow();
    }
    public string GetPath => _path;
    public void StatusitemWindow()
    {
        _visible = !_visible;
        _group.alpha = _visible ? 1 : 0;
        _group.blocksRaycasts = _visible;
        _group.interactable = _visible;
    }
}
