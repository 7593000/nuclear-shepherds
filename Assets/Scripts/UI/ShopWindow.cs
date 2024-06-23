using UnityEngine;
/// <summary>
/// Окно с наймом юнитов 
/// </summary>
public class ShopWindow : MonoBehaviour
{
    [SerializeField]
    private CardUnit _prefCardUnit;
 
    public void AddUnitsForSell( UnitConfig unit )
    {
       CardUnit card = Instantiate( _prefCardUnit, transform );
        card.Initialized( unit );
    }
}
