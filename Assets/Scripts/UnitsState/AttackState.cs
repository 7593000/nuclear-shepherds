using UnityEngine;

/// <summary>
/// ���������: ����� ����
/// </summary>
public class AttackState : IUnitState
{
    private IAttack _attack;
    private float _damage = 0;
    private float _luck;
    private bool _playSound = false;
    private bool _loop = false;
    private AudioClip[] _attackClips;

    public void EnterState( UnitComponent unit )
    {
        _attack = unit.GetAttack;
        _luck = unit.GetConfig.GetLuck;
        _attackClips = unit.GetConfig.GetWeaponsConfig.GetAudioClip;
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit( unit , StateUnitList.ATTACK );
        unit.GetGameHub.GetUnitsUpdateEngine.AddUnit( unit , StateUnitList.DIRECT );
    }

    public void ExitState( UnitComponent unit )
    {
        _playSound = false;
        if ( _loop )
        {
            SoundEngine.Instance.StopSound( SoundType.SFX , _attackClips[ 0 ] );
        }
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit( unit , StateUnitList.DIRECT );
        unit.GetGameHub.GetUnitsUpdateEngine.RemoveUnit( unit , StateUnitList.ATTACK );
    }

    public void UpdateState( UnitComponent unit )
    {
        if ( unit.GetTargetForAttack != null )
        {
            _damage = unit.GetDamageClass.DamageTarget();

            if ( _damage >= 0 )
            {
                float damageAndCrit = CalculatingDamage();


                if ( !_playSound )
                {
                    _playSound = true;

                    _loop = unit.GetConfig.GetWeaponsConfig.GetSpeedAttack <= 0;

                    SoundEngine.Instance.PlaySound( _attackClips[ 0 ] , SoundType.SFX , _loop , unit.transform );
                    if ( !_loop )
                    { _playSound = false; }
                }
                unit.StartAnimation.ToRun( StateUnit.ATTACK );

                _attack.Attack( damageAndCrit ); // �������� ���� ������ ������ ��� ��������� ����� �����
            }
            else if ( _damage == -1 ) // -1: ������ �� ��������
            {
                unit.StartAnimation.ToRun( StateUnit.IDLE );
            }
            else if ( _damage == -100 ) // -100: ������ �� ����������� 
            {
                unit.StartAnimation.ToRun( StateUnit.IDLE ); // TODO: �������� �������� �����������
            }

            if ( unit.GetTypeUnit == TypeUnit.ENEMY && unit.GetTargetForAttack.IsDead )
            {
                unit.SetState( unit.SearchState );
            }
        }
        else
        {
            unit.SetState( unit.IdleState );
        }
    }

    /// <summary>
    /// ������� ���������� �����: ���� * �����
    /// </summary>
    /// <returns>�������� �����</returns>
    private float CalculatingDamage()
    {
        return CritCalculation() ? _damage + ( _damage * _luck ) : _damage;
    }

    /// <summary>
    /// ������ �����
    /// </summary>
    /// <returns>true, ���� ����������� ���� ���������, ����� false</returns>
    private bool CritCalculation() => RandomRange() < _luck;

    /// <summary>
    /// ��������� ���������� �����
    /// </summary>
    /// <returns>��������� ����� �� 0 �� 1</returns>
    public float RandomRange()
    {
        return Random.Range( 0f , 1f );
    }
}
