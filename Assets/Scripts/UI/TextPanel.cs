
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
            _countsText = GetComponentInChildren<TMP_Text>();
            _countsText.text = value;
            Debug.LogError("TMP_Text ��� ����������.");
        }
    }
    private void Start()
    {
        if (_countsText == null)
        {
            _countsText = GetComponentInChildren<TMP_Text>();
        }

    }
}
     
   
 
