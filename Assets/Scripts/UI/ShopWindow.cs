using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Окно с наймом юнитов 
/// </summary>
public class ShopWindow : MonoBehaviour
{
    [SerializeField]
    private CardUnit _prefCardUnit;
    [SerializeField] private List<CardUnit> _cards = new();



    public void AddUnitsForSell( UnitConfig unit )
    {
        if (_prefCardUnit == unit) Debug.LogError("нет префама CardUnit");    
    
        CardUnit card = Instantiate( _prefCardUnit, transform );
        card.Initialized( unit );
        _cards.Add( card );
    }


    public void ChangingCoins(int coins)
    {
        Debug.Log( coins );
        
        foreach( CardUnit cardUnit in _cards )
        {
           

            if (cardUnit.GetPrice <= coins)
            {
             
                cardUnit.IsActive = true;
                cardUnit.GetSprite.color = Color.white;
      
                cardUnit.GetColorPrice = Color.white;
            }
            else
            {
                 
                cardUnit.IsActive = false;
                cardUnit.GetSprite.color = Color.black;
                cardUnit.GetColorPrice = Color.grey;
            }
            
        }
    }
}
