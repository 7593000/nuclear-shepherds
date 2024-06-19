 
public interface IHealth
{
    public bool IsDead {  get; set; }  
    public void TakeDamage( float damage );
    public float Health();
    
}
