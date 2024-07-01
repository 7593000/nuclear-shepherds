using UnityEngine;

public class WindowInfoUnit : MonoBehaviour
{
    // private Canvas _canvas;
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
        //_canvas = GetComponent<Canvas>();
        _group = GetComponent<CanvasGroup>();

        CanvasStatus( false );
    }

    private void CanvasStatus( bool status )
    {
        _group.alpha = status ? 1 : 0;
        _group.blocksRaycasts = status;
        _group.interactable = status;

    }

    public void CloseWindowInfo()
    {
        CanvasStatus( false );
    }
    public void UpgradeUnit()
    {
        _unit.UpdateLevel();
        ShowInfo();
    }
    public void WindowInfo( UnitComponent unit )
    {
        _unit = unit;
        CanvasStatus( true );
        ShowInfo();
    }

    private void ShowInfo()
    {
        float costUpgrade = _unit.GetConfig.GetRatio[ 0 ];
        float damageRation = _unit.GetConfig.GetRatio[ 1 ];
        float speedAttackRatio = _unit.GetConfig.GetRatio[ 2 ];
        float luckRation = _unit.GetConfig.GetRatio[ 3 ];

        float damage = _unit.GetUnitData.Damage + _unit.GetUnitData.DamageRatio;
        float speedAttack = _unit.GetUnitData.SpeedAttack + _unit.GetUnitData.SpeedAttackRatio;
        float luck = _unit.GetUnitData.Luck +_unit.GetUnitData.LuckRatio;

        float newDamageValue = damage + damageRation * (_unit.GetUnitData.Level+1);
        float newSpeedAttackValue = speedAttack + speedAttackRatio * (_unit.GetUnitData.Level+1);
        float newLuckValue = luck + luckRation * ( _unit.GetUnitData.Level + 1 );

        float cost = costUpgrade * _unit.GetUnitData.Level;

        _nameUnit.SetText( _unit.GetConfig.GetName );
        _level.SetText( _unit.GetUnitData.Level.ToString() );
        _costUdgrade.SetText( cost.ToString() );

        _currentDamage.SetText( damage.ToString() );
        _currentSpeedAttack.SetText( speedAttack.ToString() );
        _currentLuck.SetText( luck.ToString() );

        _newDamage.SetText( newDamageValue.ToString() );
        _newSpeedAttack.SetText( newSpeedAttackValue.ToString() );
        _newLuck.SetText( newLuckValue.ToString() );

    }
}
