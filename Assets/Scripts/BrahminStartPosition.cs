using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BrahminStartPosition : MonoBehaviour
{
    [SerializeField] private Tilemap _tileForPosition;
    [SerializeField] private List<Vector3Int> _cellPosition = new();
    [SerializeField] private Dictionary<Vector3Int , bool> _statusCell = new();

    private void OnValidate()
    {
        BoundsInt bounds = _tileForPosition.cellBounds;
        TileBase[] allTiles = _tileForPosition.GetTilesBlock( bounds );
        for ( int x = bounds.xMin; x < bounds.xMax; x++ )
        {
            for ( int y = bounds.yMin; y < bounds.yMax; y++ )
            {
                Vector3Int pos = new Vector3Int( x , y , 0 );
                
                if ( _tileForPosition.HasTile( pos ) )
                {
                  
                    _cellPosition.Add( pos );

                }
            }
        }
    }

    public Vector3  TransferFreeRandomCell()
    {
        
        int count = _cellPosition.Count;
      
        while ( count > 0 )
        {
            Vector3Int pos = RandomCellPosition();
        
            if ( !_statusCell.ContainsKey( pos ) )
            {
                _statusCell.Add( pos , true );
                Vector3 worldPosition = _tileForPosition.CellToWorld( pos );
                return worldPosition;
            }
            count--;
        }
        return Vector3Int.CeilToInt( transform.position );

    }

    private Vector3Int RandomCellPosition()
    {
       
        int randomIndex = Random.Range( 0 , _cellPosition.Count );

        return _cellPosition[ randomIndex ];
    }

}
