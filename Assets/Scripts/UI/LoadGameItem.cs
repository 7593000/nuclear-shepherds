using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameItem : MonoBehaviour
{
   [SerializeField] private TextPanel _textPanel;
    private CanvasGroup _group;
    private bool _visible = false;

    public void Initialized( )
    {

        _group = GetComponent<CanvasGroup>();



    }
    public void ItemStatus(string path)
    {
        
        _textPanel.SetText(path);
        _visible = !_visible;
        _group.alpha = _visible ? 1 : 0;
        _group.blocksRaycasts = _visible;
        _group.interactable = _visible;
    }
}
