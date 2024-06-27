using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField]
    private BorderType _border;

    public BorderType GetBorderType => _border;
}
