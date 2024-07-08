using UnityEngine;

public sealed class WalletEngine : MonoBehaviour
{


    private Wallet _wallet;


    public void Initialized( GameHub gameHub )
    {
        

        _wallet = new Wallet( gameHub.GetGameSettings.GetGameData.Coins );
         
        Enemy.OnCoins += ( int value ) => _wallet.AddÑurrency( value );

     
    }
    private void OnDisable()
    {
    
        Enemy.OnCoins -= ( int value ) => _wallet.AddÑurrency( value );
    }

    public Wallet GetWallet => _wallet;

}
