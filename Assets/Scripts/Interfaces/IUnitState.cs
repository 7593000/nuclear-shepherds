public interface IUnitState  
{
    /// <summary>
/// ����� ����������� ��� �������� � ��������� 
/// </summary>
    public void EnterState( UnitComponent unit );
    /// <summary>
    /// ����� ����������� ��� ���������� � ���������  
    /// </summary>
    /// <param name="bot"></param>
    public void UpdateState( UnitComponent unit );
    /// <summary>
    /// ����������� ��� ������ �� ���������  
    /// </summary>
    public void ExitState( UnitComponent unit );
     
}
