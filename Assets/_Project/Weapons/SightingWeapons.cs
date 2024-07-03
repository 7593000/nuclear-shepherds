/// <summary>
/// ����� ��� ������ ����������� ��������: ��������, ������ �������� ���
/// </summary>
public class SightingWeapons : IAttack
{
    private UnitComponent _unit;
    public SightingWeapons( UnitComponent unit )
    {
        _unit = unit;
    }

    public void Attack( float damage )
    {
        _unit.GetTargetForAttack.TakeDamage( damage );
    }
}
