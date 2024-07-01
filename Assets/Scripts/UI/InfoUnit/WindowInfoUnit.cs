using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowInfoUnit : MonoBehaviour
{
    private Canvas _canvas;
    private CanvasGroup _group;
    
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
        _canvas = GetComponent<Canvas>();
        _group = GetComponent<CanvasGroup>();

        CanvasStatus(false);
    }

    private void CanvasStatus(bool status)
    {
        
        _canvas.enabled = status;
        _group.interactable = status;

    }

    public void CloseWindow()
    {
        CanvasStatus(false) ;
    }

    public void WindowInfo(UnitComponent unit)
    {

        CanvasStatus(true);
        ShowInfo(1000, unit);
    }

    private void ShowInfo(  int costUpgrade, UnitComponent unit)
    // public void ShowInfo(string name,  int costUpgrade, float currentDamage, float currentSpeedAttack, float currentLuck)
    {
        _nameUnit.SetText(unit.GetConfig.GetName);
        _level.SetText(unit.GetUnitData.Level.ToString());
        _costUdgrade.SetText("1200");
        _currentDamage.SetText(unit.GetUnitData.Damage.ToString());
        _currentSpeedAttack.SetText(unit.GetUnitData.SpeedAttack.ToString());
        _currentLuck.SetText(unit.GetUnitData.Luck.ToString());

        string newDamage = (unit.GetUnitData.Damage  * 1.1f).ToString();
        string newSpeedAttack = (unit.GetUnitData.SpeedAttack * 1.2f).ToString();
        string newLuck = (unit.GetUnitData.Luck * 1.3f).ToString();
       
       

        _newDamage.SetText(newDamage);
        _newSpeedAttack.SetText(newSpeedAttack.ToString());
        _newLuck.SetText(newLuck.ToString());

    }
}
