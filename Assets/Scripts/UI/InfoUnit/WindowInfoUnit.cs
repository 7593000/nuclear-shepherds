using UnityEngine;
using UnityEngine.UI;

public class WindowInfoUnit : MonoBehaviour
{

    private CanvasGroup _group;
    private UnitComponent _unit;
     
    [Space]
    [SerializeField] private TextPanel _nameUnit;
    [SerializeField] private TextPanel _level;
    [SerializeField] private TextPanel _costUdgrade;
    [SerializeField] private TextPanel _currentDamage;
    [SerializeField] private TextPanel _currentSpeedAttack;
    [SerializeField] private TextPanel _currentLuck;
    [SerializeField] private TextPanel _newDamage;
    [SerializeField] private TextPanel _newSpeedAttack;
    [SerializeField] private TextPanel _newLuck;

    [SerializeField] private Button _upgradeButton;

    private void Start()
    {

        _group = GetComponent<CanvasGroup>();

        CanvasStatus(false);
    }

    /// <summary>
    /// Активность меню , показать - скрыть
    /// </summary>
    private void CanvasStatus(bool status)
    {
        _group.alpha = status ? 1 : 0;
        _group.blocksRaycasts = status;
        _group.interactable = status;

    }
    public void DismissUnit()
    {
        //Vector3Int positionUnit = Vector3Int.CeilToInt(_unit.transform.position);
     
        _unit.GetGameHub.GetTileMap.RemoveCell(_unit.CellPosition);
        _unit.DeactiveUnit();

        CanvasStatus(false);

    }

    public void CloseWindowInfo()
    {
        CanvasStatus(false);
    }
    public void UpgradeUnit()
    {
        int costUpdate = ( int )_unit.GetConfig.GetRatio[ 0 ] * _unit.GetUnitData.Level ;
        
        if (  CheckedLevel() || !CheckedCouns( costUpdate ) )
        {
           
            return;
        }
        int level = _unit.GetUnitData.Level;
        level++;
        _unit.UpdateLevel( level );
      
        ShowInfo();
    }
    public void WindowInfo(UnitComponent unit)
    {
        _unit = unit;
         
         
       

       
        CanvasStatus(true);
        ShowInfo();
    }

    private void ShowInfo()
    {
       


        bool isMaxLevel = CheckedLevel();
        string maxLevelText = "MAX";

        float costUpgrade = _unit.GetConfig.GetRatio[0];
        float damageRation = _unit.GetConfig.GetRatio[1];
        float speedAttackRatio = _unit.GetConfig.GetRatio[2];
        float luckRation = _unit.GetConfig.GetRatio[3];

        float damage = _unit.GetUnitData.Damage + _unit.GetUnitData.DamageRatio;
        float speedAttack = Mathf.Max(0, _unit.GetUnitData.SpeedAttack - _unit.GetUnitData.SpeedAttackRatio);
        float luck = _unit.GetUnitData.Luck + _unit.GetUnitData.LuckRatio;

        string newDamageValue = isMaxLevel ? maxLevelText : (damage + damageRation * (_unit.GetUnitData.Level)).ToString("F1");
        string newSpeedAttackValue = isMaxLevel ? maxLevelText : Mathf.Max(0, speedAttack - speedAttackRatio * (_unit.GetUnitData.Level)).ToString("F1");
        string newLuckValue = isMaxLevel ? maxLevelText : (luck + luckRation * (_unit.GetUnitData.Level)).ToString("F1");
        string cost = isMaxLevel ? maxLevelText : (costUpgrade * _unit.GetUnitData.Level).ToString("F1");

        _nameUnit.SetText(_unit.GetConfig.GetName);
        _level.SetText(_unit.GetUnitData.Level.ToString());
        _costUdgrade.SetText(cost);

        _currentDamage.SetText(damage.ToString("F2"));
        _currentSpeedAttack.SetText(speedAttack.ToString("F2"));
        _currentLuck.SetText(luck.ToString("F2"));

        _newDamage.SetText(newDamageValue);
        _newSpeedAttack.SetText(newSpeedAttackValue);
        _newLuck.SetText(newLuckValue);
    }


    /// <summary>
    /// Проверка уровня юнита, если максимальный, то блочим возможность повысить уровень 
    /// </summary>
    /// <returns></returns>
    private bool CheckedLevel()
    {
        int currentLevel = _unit.GetUnitData.Level;
        int maxLevel = _unit.GetGameHub.GetGameSettings.GetMaxLevel;

      
        return currentLevel >=  maxLevel;
    }

    /// <summary>
    /// Проверка возможности потратить деньги
    /// </summary>
   
    private bool CheckedCouns( float coint)
    {
        return coint <= _unit.GetGameHub.GetWalletEngine.GetWallet.Coins;
    }
}
