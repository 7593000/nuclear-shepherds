using System.Collections.Generic;
using UnityEngine;

public class PoolExplosion : MonoBehaviour
{
    [SerializeField] private Explosion _explosionPref;
    [SerializeField] private Transform _parent;
    [SerializeField, Tooltip("���������� ����������� ��������� � ����")] private int _countElements = 10;

    private List<Explosion> _explosionList = new();

    private void Start()
    {
        // �������� ������ ������� ��� �������� ����� �������, ���� �� �� �����
        if (_parent == null)
        {
            _parent = new GameObject("ExplosionPool").transform;
        }

        CreatePool(_countElements);
    }

    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var exp = InstantiateObj();
            if (exp != null)
            {
                _explosionList.Add(exp);
                exp.gameObject.SetActive(false); // �������� ������ ����� ��������
            }
        }
    }

    private Explosion InstantiateObj()
    {
        Explosion exp = Instantiate(_explosionPref, _parent);
        if (exp == null) return null;
        return exp;
    }




    public Explosion GetExplosion()
    {
        // ����� ����������� ������ � ����
        for (int i = 0; i < _explosionList.Count; i++)
        {
            if (!_explosionList[i].gameObject.activeSelf)
            {
                Debug.Log("���� ����� : " + _explosionList[i].name);
               
                return _explosionList[i];
            }
        }

        // ���� ��� ������ �������, ������ ����� � ��������� ��� � ���
        Explosion temp = InstantiateObj();
        if (temp != null)
        {
            _explosionList.Add(temp);
            temp.gameObject.SetActive(false);
            Debug.Log("�������� ������ ������ ��� �����: " + temp.name);
        }

        return temp;
    }
}
