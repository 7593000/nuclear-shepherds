 
public interface IHealth
{
    public bool IsDead {  get; set; }  
    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="damage">�������� �����</param>
    public void TakeDamage( TypeWeapons type, float damage);
    /// <summary>
    /// �������� ���������� ������
    /// </summary>
    /// <returns></returns>
    public float Health();
    
}
