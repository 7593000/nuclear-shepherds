using System.Collections.Generic;
using UnityEngine;

public class PoolExplosion : MonoBehaviour
{
    [SerializeField] private Explosion _explosionPref;
    [SerializeField] private Transform _parent;
    [SerializeField, Tooltip("Количество создаваемых элементов в пуле")] private int _countElements = 10;

    private List<Explosion> _explosionList = new();

    private void Start()
    {
        // Создание нового объекта для хранения пулов взрывов, если он не задан
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
                exp.gameObject.SetActive(false); // Скрываем объект после создания
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
        // Поиск неактивного взрыва в пуле
        for (int i = 0; i < _explosionList.Count; i++)
        {
            if (!_explosionList[i].gameObject.activeSelf)
            {
                Debug.Log("Взят врзыв : " + _explosionList[i].name);
               
                return _explosionList[i];
            }
        }

        // Если все взрывы активны, создаём новый и добавляем его в пул
        Explosion temp = InstantiateObj();
        if (temp != null)
        {
            _explosionList.Add(temp);
            temp.gameObject.SetActive(false);
            Debug.Log("Создание нового взрыва для пулла: " + temp.name);
        }

        return temp;
    }
}
