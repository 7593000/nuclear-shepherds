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
    private float _distance;

    public UnitConfig GetConfig => _config;
    public Image GetSprite => _image;
    public float GetDistance => _distance;
    public int GetPrice => GetConfig.GetCost;
    public Color GetColorPrice{ get=>_price.color;set => _price.color = value;}
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Инициализация карточки юнита 
    /// </summary>
    /// <param name="sprite">Картинка юнита</param>
    /// <param name="price">Стоиомость найма</param>
    public void Initialized(UnitConfig config)
    {
        _config = config;
      
        _name = config.GetName;
        _price.text = config.GetCost.ToString();
        _image.sprite = config.GetSprite;
        _typeWeapon = config.GetTypeWeapons.ToString();
        _damage = config.GetWeaponsConfig.GetDamage.ToString();
        _luck = (config.GetLuck * 100).ToString();
        _distance = config.GetWeaponsConfig.GetDistance;


    }

}
