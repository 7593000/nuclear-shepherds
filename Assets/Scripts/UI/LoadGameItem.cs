using UnityEngine;

public class LoadGameItem : MonoBehaviour
{
    [SerializeField] private TextPanel _textPanel;

    private CanvasGroup _group; 
    private string _path;
    public string GetPath => _path;

    public void Initialized()
    {
        _group = GetComponent<CanvasGroup>();
    }
    public void ItemStatus(string path = null)
    {
        _path = path;
        _textPanel.SetText(path);
     
    }

    public void ShowHideItems(bool visible)
    {
        _group.alpha = visible ? 1 : 0;
        _group.blocksRaycasts = visible;
        _group.interactable = visible;
    }

 
}
