using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrahminManager : MonoBehaviour
{

    [SerializeField,Tooltip("���������� �������� � ����")]
    private int _countBrahmin = 6;

    [SerializeField]    
    private List<Brahmin> _brahminList = new();


   


    public IReadOnlyList<Brahmin> GetBrahminList => _brahminList;
    



}
