
using TMPro;
using UnityEngine;

public abstract class TextPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _countsText;


    public void SetText(string value)
    {
        if (_countsText != null)
        {
            _countsText.text = value ;
        }
        else
        {
            Debug.LogError("TMP_Text нет компонента.");
        }
    }
    private void OnValidate()
    {
        if (_countsText == null)
        {
            _countsText = GetComponentInChildren<TMP_Text>();
        }

    }
}
     
   
 
