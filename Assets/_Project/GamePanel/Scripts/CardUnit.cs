using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUnit : CardUnitComponent
{

    private UnitConfig _config;
    [SerializeField]
    private TMP_Text _price;
    [SerializeField]

    private Image _image;
    private int _occupiedArea;
    private float _distance;

    public string GetPrice => _price.ToString();
    public Sprite GetSprite => _image.sprite;
    public float GetDistance => _distance;
    public UnitConfig GetConfig=> _config;
    /// <summary>
    /// Инициализация карточки юнита 
    /// </summary>
    /// <param name="sprite">Картинка юнита</param>
    /// <param name="price">Стоиомость найма</param>
    /// <param name="occupiedArea">Количество занимаемых клеток на поле </param>
    public void Initialized( UnitConfig config )
    {
        _config = config;
        _name = config.GetName;
        _price.text = config.GetCost.ToString();
        _image.sprite = config.GetSprite;
        _occupiedArea = config.GetOccupiedArea; //todo => del
        _typeWeapon = config.GetTypeWeapons.ToString();
        _damage = config.GetWeaponsConfig.GetDamage.ToString();
        _luck = ( config.GetLuck * 100 ).ToString();
        _distance = config.GetDistance;

    }
}
