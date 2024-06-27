using TMPro;
using UnityEngine;

public class WalletPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _countsText;

    public void SetText(int value) { _countsText.text = value.ToString(); }




    private void Awake()
    {
        _countsText??=GetComponent<TMP_Text>();
    }
}
