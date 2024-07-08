using System;

/// <summary>
/// ����� ��� ��������-�������� �������� ������ � ���� 
/// </summary>
public class Wallet
{
    public event Action<int> OnCoinsChanged;

    public Wallet( int count )
    {
        _coins = count;
    }



    private int _coins;

    /// <summary>
    /// �������� ������ � �������
    /// </summary>
    /// <param name="count">���������� ����������� ������</param>
    public void AddCurrency( int count )
    {

        if ( count > 0 )
        {
            Coins += count;

        }
    }

    /// <summary>
    /// ������ ����� �� ��������
    /// </summary>
    /// <param name="count">���������� ���������� ������</param>
    /// <returns></returns>
    public bool TakeCurrency( int count )
    {
        if ( count < 0 )
        {
            return false;
        }
        else if ( count > _coins )
        {
            return false;
        }

        Coins -= count;
        return true;

    }

    /// <summary>
    /// �������� ��� ��������� ���������� �����
    /// </summary>
    /// <returns></returns>
    public int Coins
    {
        get => _coins;
        private set
        {
            _coins = value;
            OnCoinsChanged?.Invoke( _coins );
        }
    }
}
