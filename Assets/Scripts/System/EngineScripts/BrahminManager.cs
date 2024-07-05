using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrahminManager : MonoBehaviour
{
    public event Action<int> OnBrahmin; 

    [SerializeField] private BrahminStartPosition _position;
    [SerializeField] Brahmin _brahminPrefab;
    [SerializeField] Transform _parent;
    [ SerializeField,Tooltip("���������� �������� � ����"),Range(1,10f)]
    private int _countBrahmin = 6;

    [SerializeField]    
    private List<Brahmin> _brahminList = new();
     
    public IReadOnlyList<Brahmin> GetBrahminList => _brahminList;

    private void OnValidate()
    {
        _position ??= FindFirstObjectByType<BrahminStartPosition>();
    }
    public void Initialized(GameHub gameHub)
    {
        for(int i = 0; i< _countBrahmin; i++)  
            {
            Vector3  positionCell = _position.TransferFreeRandomCell();
            
            Brahmin brahmin = Instantiate( _brahminPrefab , positionCell ,Quaternion.identity );
            brahmin.transform.SetParent( _parent );
            brahmin.Initialized(this);
            _brahminList.Add( brahmin );

            }
        OnBrahmin?.Invoke(_brahminList.Count);
    }

    public void DeadBrahmin(Brahmin brahmin) {

        if (_brahminList.Count == 0)
            Debug.Log("GAMEOVER");

        if(_brahminList.Contains(brahmin))
        {
            _brahminList.Remove(brahmin);

            OnBrahmin?.Invoke(_brahminList.Count);
        }

    }


}
