using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class TileMapEngine : MonoBehaviour
{
    public Tilemap _tilemap;
    private TilemapRenderer _tilemapRenderer;
    private Vector3Int _lastHighlightedCell;
    private bool _hasHighlightedCell = false;
    [SerializeField] private bool _canPlace = false; ///����������� ���������� ����� �� ������� 
    [SerializeField] private List<Vector3Int> _occupiedCells = new(); //���� � �������� ��������


    // �������� ������ ��� ������ � �������� �����
    private readonly Vector3Int[] _neighborCellsOdd =
   {
        new (0, 0, 0), //�����������
        new (1, 0, 0),  // �
        new (-1, 0, 0), // �
        new (1, 1, 0),  // ��
        new (0, 1, 0),  // ��
        new (1, -1, 0), // ��
        new (0, -1, 0)  // ��
    };

    private readonly Vector3Int[] _neighborCellsEven =
    {
        new (0, 0, 0),//�����������
        new (1, 0, 0),  // �
        new (-1, 0, 0), // �
        new (0, 1, 0),  // ��
        new (-1, 1, 0), // ��
        new (0, -1, 0), // ��
        new (-1, -1, 0)  // ��
    };

    /// <summary>
    /// �������� - ��������� ������ tilemap
    /// </summary>
    /// <param name="status"></param>
    public void TileMapActivity(bool status)
    {
        _tilemapRenderer.enabled = status;

    }

    /// <summary>
    /// �������� ������ ������, �������� �� ��������� �� ��� ���� ��� ���
    /// </summary>
    /// <returns></returns>
    public bool CheckedCell()
    {
        ClearHighlightNeighbors(_lastHighlightedCell, ParityCheckAxisY(_lastHighlightedCell.y));
        return _canPlace;

    }


    /// <summary>
    /// �������� ������� ������ ��� ������ ��� ���������
    /// </summary>
    /// <returns> Vector3 </returns>
    public Vector3 GetPositionCell()
    {
        _canPlace = false;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = _tilemap.WorldToCell(mouseWorldPos);

        if (_hasHighlightedCell && cellPosition != _lastHighlightedCell)
        {
            ClearHighlightNeighbors(_lastHighlightedCell, ParityCheckAxisY(_lastHighlightedCell.y));
            _hasHighlightedCell = false;
        }

        if (_tilemap.GetTile(cellPosition) != null)
        {
            HighlightNeighbors(cellPosition, ParityCheckAxisY(cellPosition.y));
            _lastHighlightedCell = cellPosition;
            _hasHighlightedCell = true;
        }

        Vector3 worldPositionCell = _tilemap.CellToWorld(_lastHighlightedCell);

        if (_canPlace)
        {
            return worldPositionCell;
        }
        
        return mouseWorldPos;
        
    }
    /// <summary>
    /// �������� �� �������� �� ��� Y. 
    /// ���� ��������, �� ���� �������� ��� �������� �����
    /// </summary>
    /// <param name="y">�������� ���������� �� ��� Y ��� ��������� ������</param>
    /// <returns>������ � ��������� ��� �������� �����</returns>
    private Vector3Int[] ParityCheckAxisY(int y)
    {
        return (y % 2 == 0) ? _neighborCellsEven : _neighborCellsOdd;
    }

    /// <summary>
    /// ������� ������� � ��������� ��� �������
    /// </summary>
    /// <param name="cellPosition">���������� ������</param>
    /// <param name="neighbors"> ������ �������� �������</param>
    private void HighlightNeighbors(Vector3Int cellPosition, Vector3Int[] neighbors)
    {
        bool shouldHighlightRed = false;

        foreach (var direction in neighbors)
        {
            Vector3Int neighborPosition = cellPosition + direction;
            if (_occupiedCells.Contains(neighborPosition) || _tilemap.GetTile(neighborPosition) == null)
            {
                shouldHighlightRed = true;
                break;
            }
        }

        foreach (var direction in neighbors)
        {
            Vector3Int neighborPosition = cellPosition + direction;
            HighlightTile(neighborPosition, shouldHighlightRed);
        }
    }

    /// <summary>
    /// �������� ������ � ���� � �������� ��������
    /// </summary>
    /// <param name="cellPosition"></param>
    /// <param name="neighbors"></param>
    public void AddCell(Vector3Int cellPosition)
    {
        var neighbors = ParityCheckAxisY(_lastHighlightedCell.y);

        foreach (var direction in neighbors)
        {
            Vector3Int neighborPosition = cellPosition + direction;
            TileBase currentTile = _tilemap.GetTile(neighborPosition);

            if (currentTile != null) _occupiedCells.Add(neighborPosition);


        }

    }
    /// <summary>
    /// �������� ����� �� ������ ������� 
    /// </summary>
    /// <param name="cellPosition"></param>
    /// <param name="neighbors"></param>
    public void RemoveCell(Vector3Int cellPosition)
    {

        var neighbors = ParityCheckAxisY(cellPosition.y);

        foreach (var direction in neighbors)
        {
            Vector3Int neighborPosition = cellPosition + direction;
            TileBase currentTile = _tilemap.GetTile(neighborPosition);
            if (currentTile != null)
            {
                _occupiedCells.Remove(neighborPosition);
            }
        }

    }



    /// <summary>
    /// ������� ������� � ������� ����� ��� ������ ������ 
    /// </summary>
    /// <param name="cellPosition">��������� ������</param>
    /// <param name="neighbors">������ � ��������� �������</param>
    private void ClearHighlightNeighbors(Vector3Int cellPosition, Vector3Int[] neighbors)
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
    private void ClearHighlightTile(Vector3Int cellPosition)
    {
        TileBase currentTile = _tilemap.GetTile(cellPosition);

        if (currentTile != null)
        {
            _tilemap.SetTileFlags(cellPosition, TileFlags.None); // ��������� ��������� �����
            _tilemap.SetColor(cellPosition, Color.white); // ���������� ���� ����� �� �����


        }
        //  _canPlace = true;
    }


    /// <summary>
    /// �������� �����
    /// </summary>
    /// <param name="cellPosition"></param>
    private void HighlightTile(Vector3Int cellPosition, bool shouldHighlightRed)
    {
        TileBase currentTile = _tilemap.GetTile(cellPosition);

        if (currentTile != null)
        {
            _tilemap.SetTileFlags(cellPosition, TileFlags.None); // ��������� ��������� �����
            _tilemap.SetColor(cellPosition, shouldHighlightRed ? Color.red : Color.green); // ������������� ����
            _canPlace = !shouldHighlightRed;
        }
        else
        {
            _tilemap.SetTileFlags(cellPosition, TileFlags.None);
            _tilemap.SetColor(cellPosition, Color.red);
            _canPlace = false;
            Debug.Log("������ �� ����������!");
        }
    }



    private void Start()
    {
        _tilemapRenderer = _tilemap.GetComponent<TilemapRenderer>();
        _tilemapRenderer.enabled = false;
    }
}
