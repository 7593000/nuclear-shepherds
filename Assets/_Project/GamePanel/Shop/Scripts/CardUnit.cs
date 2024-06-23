using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUnit : CardUnitComponent
{


    [SerializeField]
    private TMP_Text _price;
    [SerializeField]

    private Image _image;
    private int _occupiedArea;

 
    public string GetPrice => _price.ToString();
    public Sprite GetSprite => _image.sprite;
 
    
    /// <summary>
    /// ������������� �������� ����� 
    /// </summary>
    /// <param name="sprite">�������� �����</param>
    /// <param name="price">���������� �����</param>
    /// <param name="occupiedArea">���������� ���������� ������ �� ���� </param>
    public void Initialized( UnitConfig config )
    {
        _name = config.GetName;
        _price.text = config.GetCost.ToString();
        _image.sprite = config.GetSprite;
        _occupiedArea = config.GetOccupiedArea;
        _typeWeapon = config.GetTypeWeapons.ToString();
        _damage = config.GetWeaponsConfig.GetDamage.ToString();
        _luck = (config.GetLuck *100).ToString();

    }
}
