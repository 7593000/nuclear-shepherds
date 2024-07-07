using System;
using UnityEngine;

public sealed class WalletEngine : MonoBehaviour
{
  

     private Wallet _wallet;


    public void Initialized(int value)
    {
        _wallet = new Wallet(value);

        Enemy.OnCoins +=(int value)=> _wallet.AddÑurrency(value);
    }


    private void OnDestroy()
    {
        Enemy.OnCoins -= (int value) => _wallet.AddÑurrency(value);
    }

    public Wallet GetWallet => _wallet;

}
