using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    private IHealth _healthUnit;
    [SerializeField]
    private Image _healthFilled;
    [SerializeField] private float MaxHealth;
    [SerializeField] public float CurrentHealth { get; private set; }

    public void Container( IHealth unit )
    {
        _healthUnit = unit;
        MaxHealth = CurrentHealth = unit.Health();
        UpdateHealthVisual( CurrentHealth , MaxHealth );
    }

    public float TakeDamage( float damage )
    {

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            UnitIsDead();
        }
        UpdateHealthVisual( CurrentHealth , MaxHealth );
        
        return CurrentHealth;
    }

    private void UnitIsDead()
    {
       
        Debug.Log( "Цель мертва" );
        
        _healthUnit.IsDead = true;
         HealthBotVisible(false);

        //TODO => проверка в другом классе на количвество браминов.  
    }

    public void UpdateHealthVisual( float currentHealth , float maxHealth )
    {
        if ( currentHealth <= 0 )
        {
            _healthFilled.fillAmount = 0;
            return;
        }
        _healthFilled.fillAmount = currentHealth / maxHealth;
    }

    public void HealthBotVisible( bool visible )
    {

        GetComponent<Image>().enabled = visible;
    }
}
