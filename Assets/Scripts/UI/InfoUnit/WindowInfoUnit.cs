using UnityEngine;

public class WindowInfoUnit : MonoBehaviour
{

    private CanvasGroup _group;
    private UnitComponent _unit;


    [SerializeField] private float _cost = 1000f;
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



    private void Start()
    {

        _group = GetComponent<CanvasGroup>();

        CanvasStatus(false);
    }

    /// <summary>
    /// јктивность меню , показать - скрыть
    /// </summary>
    private void CanvasStatus(bool status)
    {
        _group.alpha = status ? 1 : 0;
        _group.blocksRaycasts = status;
        _group.interactable = status;

    }
    public void DismissUnit()
    {
        Vector3Int positionUnit = Vector3Int.CeilToInt(_unit.transform.position);
     
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
        if (CheckedLevel()) { return; }
     
        _unit.UpdateLevel();
      
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

        _currentDamage.SetText(damage.ToString("F1"));
        _currentSpeedAttack.SetText(speedAttack.ToString("F1"));
        _currentLuck.SetText(luck.ToString("F1"));

        _newDamage.SetText(newDamageValue);
        _newSpeedAttack.SetText(newSpeedAttackValue);
        _newLuck.SetText(newLuckValue);
    }


    /// <summary>
    /// ѕроверка уровн€ юнита, если максимальный, то блочим возможность повысить уровень 
    /// </summary>
    /// <returns></returns>
    private bool CheckedLevel()
    {
        int maxLevel = _unit.GetGameHub.GetGameData.GetMaxLevel;
        int currentLevel = _unit.GetUnitData.Level;

        return currentLevel >= maxLevel;
    }
}
