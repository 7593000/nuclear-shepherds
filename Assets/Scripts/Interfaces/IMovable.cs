

using UnityEditor;
using UnityEngine;

public interface IMovable
{
    GameObject unit { get; }
    void Move();
    public void UpdateUnit();


}
