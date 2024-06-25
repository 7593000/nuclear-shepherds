using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapEngine : MonoBehaviour
{
    public Tilemap tilemap;
    private Vector3Int _lastHighlightedCell;
    private bool _hasHighlightedCell = false;
   
    // �������� ������ ��� ������ � �������� �����

    private readonly Vector3Int[] _neighborCellsOdd =
    {
        new (0, 0, 0), //�����������
        new (1, 0, 0),  // Right
        new (-1, 0, 0), // Left
        new (1, 1, 0),  // Top Right
        new (0, 1, 0),  // Top Left
        new (1, -1, 0), // Bottom Right
        new (0, -1, 0)  // Bottom Left
    };

    private readonly Vector3Int[] _neighborCellsEven =
    {
        new (0, 0, 0),//�����������
        new (1, 0, 0),  // Right
        new (-1, 0, 0), // Left
        new (0, 1, 0),  // Top Right
        new (-1, 1, 0), // Top Left
        new (0, -1, 0), // Bottom Right
        new (-1, -1, 0)  // Bottom Left
    };

    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);


        if (Input.GetMouseButtonUp(0)) // ���� ������ ����� ������ ����
        {

            HighlightNeighbors(cellPosition, ParityCheckAxisY(cellPosition.y));
        }
         
          
        if (_hasHighlightedCell && cellPosition != _lastHighlightedCell)
        {
            ClearHighlightNeighbors(_lastHighlightedCell,  ParityCheckAxisY(_lastHighlightedCell.y));
           
            _hasHighlightedCell = false;
        }

        if (tilemap.GetTile(cellPosition) != null)
        {
          
            HighlightNeighbors(cellPosition, ParityCheckAxisY(cellPosition.y));
          
            _lastHighlightedCell = cellPosition;
            _hasHighlightedCell = true;

        }
    }

    /// <summary>
    /// �������� �� �������� �� ��� Y. 
    /// ���� ��������, �� ���� �������� ��� �������� �����
    /// </summary>
    /// <param name="y">�������� ���������� �� ��� Y ��� ��������� ������</param>
    /// <returns>������ � ��������� ��� �������� �����</returns>
     private Vector3Int[] ParityCheckAxisY(int y)
    {
        Vector3Int[] neighborCells;

        if ( y % 2 == 0)
        {
            neighborCells = _neighborCellsEven;
        }
        else
        {
            neighborCells = _neighborCellsOdd ;
        }
        return neighborCells;
    }

    /// <summary>
    /// ������� ������� � ��������� ��� �������
    /// </summary>
    /// <param name="cellPosition">���������� ������</param>
    /// <param name="neighbors"> ������ �������� �������</param>
    void HighlightNeighbors(Vector3Int cellPosition, Vector3Int[] neighbors, bool lockCell = false)
    {
        foreach (var direction in neighbors)
        {
            Vector3Int neighborPosition = cellPosition + direction;
            HighlightTile(neighborPosition);
        }
        HighlightTile(cellPosition);
    }




     /// <summary>
     /// ������� ������� � ������� ����� ��� ������ ������ 
     /// </summary>
     /// <param name="cellPosition">��������� ������</param>
     /// <param name="neighbors">������ � ��������� �������</param>
    void ClearHighlightNeighbors(Vector3Int cellPosition, Vector3Int[] neighbors)
    {
        foreach (var direction in neighbors)
        {
            Vector3Int neighborPosition = cellPosition + direction;
         
            ClearHighlightTile(neighborPosition);
        }
    }

     

    /// <summary>
    /// �������� ������
    /// </summary>
    /// <param name="cellPosition"></param>
    void ClearHighlightTile(Vector3Int cellPosition)
    {
        TileBase currentTile = tilemap.GetTile(cellPosition);

        if (currentTile != null)
        {
            tilemap.SetTileFlags(cellPosition, TileFlags.None); // ��������� ��������� �����
            tilemap.SetColor(cellPosition, Color.white); // ���������� ���� ����� �� �����
        }
    }

     
    /// <summary>
    /// �������� �����
    /// </summary>
    /// <param name="cellPosition"></param>
    void HighlightTile(Vector3Int cellPosition)
    {
        TileBase currentTile = tilemap.GetTile(cellPosition);

        if (currentTile != null)
        {
            // ��������� ����� �����
            tilemap.SetTileFlags(cellPosition, TileFlags.None); // ��������� ��������� �����
             
 
                tilemap.SetColor(cellPosition, Color.green); // �������� ���� ����� �� �������
            
        }
        else
        {
            Debug.Log("������ �� �����������!");
        }
    }
}
