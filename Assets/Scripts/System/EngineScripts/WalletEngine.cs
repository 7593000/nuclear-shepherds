using UnityEngine;

public sealed class WalletEngine : MonoBehaviour
{


    private Wallet _wallet;


    public void Initialized(GameHub gameHub)
    {
        _wallet = new Wallet(gameHub.GetGameSettings.GetGameData.Coins);
        Debug.Log("WalletEngine Инициализирован , количество монет: " + _wallet.Coins);
    }


    private void OnEnable()
    {
        Enemy.OnDeath += OnCoinsAdded;
    }

    private void OnDisable()
    {
        Enemy.OnDeath -= OnCoinsAdded;
    }

    public Wallet GetWallet => _wallet;


    public void Cleanup()
    {
        Enemy.OnDeath -= (int value) => _wallet.AddCurrency(value);

    }
    private void OnCoinsAdded(int value)
    {
        _wallet.AddCurrency(value);
    }
    /// <summary>
    /// Чит на деньги
    /// </summary>
   
    public void MoreMoney(int value)
    {
        _wallet.AddCurrency(value);
    }
}
