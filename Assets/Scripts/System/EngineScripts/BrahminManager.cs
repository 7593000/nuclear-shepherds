using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrahminManager : MonoBehaviour
{
    [SerializeField] private BrahminStartPosition _position;
    [SerializeField] Brahmin _brahminPrefab;
    [SerializeField] Transform _parent;
    [ SerializeField,Tooltip("Количество браминов в игре"),Range(1,10f)]
    private int _countBrahmin = 6;

    [SerializeField]    
    private List<Brahmin> _brahminList = new();
     
    public IReadOnlyList<Brahmin> GetBrahminList => _brahminList;

    private void OnValidate()
    {
        _position ??= FindFirstObjectByType<BrahminStartPosition>();
    }
    public void Initialized()
    {
        for(int i = 0; i< _countBrahmin; i++)  
            {
            Vector3  positionCell = _position.TransferFreeRandomCell();
            
            Brahmin brahmin = Instantiate( _brahminPrefab , positionCell ,Quaternion.identity );
            brahmin.transform.SetParent( _parent ); 
            _brahminList.Add( brahmin );
            }
       
    }


}
