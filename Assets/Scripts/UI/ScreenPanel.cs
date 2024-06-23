using TMPro;
using UnityEngine;

public class ScreenPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _infoText;

    public void ShowText( string text )
    {

        _infoText.text = text;
    }

}
