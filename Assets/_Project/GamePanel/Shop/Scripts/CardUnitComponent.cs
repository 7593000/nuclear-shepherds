using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CardUnitComponent : MonoBehaviour
{

    protected string _name;
    protected string _typeWeapon;
    protected string _damage;
    protected string _luck;


    public string GetName => _name;
    public string GetTypeWeapon => _typeWeapon;
    public string GetDamage => _damage;
    public string GetLuch => _luck;

}
