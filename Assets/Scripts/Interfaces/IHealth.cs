 
public interface IHealth
{
    public bool IsDead {  get; set; }  
    /// <summary>
    /// Получить урон
    /// </summary>
    /// <param name="damage">значение урона</param>
    public void TakeDamage( TypeWeapons type, float damage);
    /// <summary>
    /// Передать количество жизней
    /// </summary>
    /// <returns></returns>
    public float Health();
    
}
