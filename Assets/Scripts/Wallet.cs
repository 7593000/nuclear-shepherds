using System;

/// <summary>
/// Класс для кручения-верчения денежных едениц в егре 
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
    /// Добавить монеты в кошелек
    /// </summary>
    /// <param name="count">Количество добавляемой валюты</param>
    public void AddCurrency( int count )
    {

        if ( count > 0 )
        {
            Coins += count;

        }
    }

    /// <summary>
    /// Снятия монет из кошелька
    /// </summary>
    /// <param name="count">Количество забираемой валюты</param>
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
    /// Свойство для получения количества монет
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
