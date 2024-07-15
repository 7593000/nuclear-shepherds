using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class BrahminManager : MonoBehaviour
{
    public event Action<int> OnBrahmin;
    private GameHub _gameHub;
    [SerializeField] private BrahminStartPosition _position;
    [SerializeField] private Brahmin _brahminPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField, Tooltip( "Количество браминов в игре" ), Range( 1 , 10f )]
    private int _countBrahmin;

    [SerializeField]
    private List<Brahmin> _brahminList = new();

    public IReadOnlyList<Brahmin> GetBrahminList => _brahminList;

    private void OnValidate()
    {
        _position ??= FindFirstObjectByType<BrahminStartPosition>();
    }


    public void Initialized( GameHub gameHub )
    {
        _gameHub = gameHub;
        _countBrahmin = _gameHub.GetGameSettings.GetGameData.Brahmin;
        for ( int i = 0; i < _countBrahmin; i++ )
        {
            Vector3 positionCell = _position.TransferFreeRandomCell();

            Brahmin brahmin = Instantiate( _brahminPrefab , positionCell , Quaternion.identity );
            brahmin.transform.SetParent( _parent );
           brahmin.Initialized( _gameHub );
            _brahminList.Add( brahmin );
           

        }
        OnBrahmin?.Invoke( _brahminList.Count );
    }

    public void DeadBrahmin( Brahmin brahmin )
    {




        if ( _brahminList.Contains( brahmin ) )
        {
            _brahminList.Remove( brahmin );

            OnBrahmin?.Invoke( _brahminList.Count );
        }


        if ( _brahminList.Count == 0 )
        {

            _gameHub.GetGameSettings.LoadSceneAsync("GameOver");

 
        }
    }


}
