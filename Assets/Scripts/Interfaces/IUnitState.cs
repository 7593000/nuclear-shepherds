public interface IUnitState  
{
    /// <summary>
/// метод выполняется при переходе в состояние 
/// </summary>
    public void EnterState( UnitComponent unit );
    /// <summary>
    /// метод выполняемый при нахождении в состоянии  
    /// </summary>
    /// <param name="bot"></param>
    public void UpdateState( UnitComponent unit );
    /// <summary>
    /// выполняемый при выходе из состояния  
    /// </summary>
    public void ExitState( UnitComponent unit );
     
}
