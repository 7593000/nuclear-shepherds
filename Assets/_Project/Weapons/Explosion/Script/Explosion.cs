using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField, Tooltip("Радиус поражения взрыва")]
    private float _explosionRadius = 2f;
    
    private List<IHealth> _strikingObjects = new();

  

    public List<IHealth> Explode()
    {
       
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.TryGetComponent<IHealth>(out IHealth enemyHealth))
            {
                _strikingObjects.Add(enemyHealth);
               
            }
        }
        

        return _strikingObjects;

        
    }

    public void Deactivation()
    {
        
        gameObject.SetActive(false);
    }



 

}
