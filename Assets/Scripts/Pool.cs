using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _container;

    [SerializeField] private int _minCapasity;
    [SerializeField] private int _maxCapasity;

    [SerializeField] private bool _autoExpand;

    private List<GameObject> _pools;

    #region Mono
    private void OnValidate()
    {
        if (_minCapasity < 0)
            _minCapasity *= -1;

        if (_autoExpand)
            _maxCapasity = int.MaxValue;
    }

    private void Awake()
    {
        CreatePool();
    }

    #endregion

    #region Private Method
    private void CreatePool()
    {
        _pools = new List<GameObject>(_minCapasity);

        for(int i = 0; i < _minCapasity; i++)
        {
            CreateElement();
        }
    }

    private GameObject CreateElement(bool isActiveBuDefault = false)
    {
        var createdObj = Instantiate(_prefab, _container.transform.position, Quaternion.identity);
        createdObj.gameObject.SetActive(isActiveBuDefault);

        _pools.Add(createdObj);

        return createdObj;
    }

    #endregion

    #region Public Method
    public bool TryGetElement(out GameObject element)
    {
        foreach(var item in _pools)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public GameObject GetFreeElement()
    {
        if(TryGetElement(out var element))
        {
            return element;
        }

        if (_autoExpand)
        {
            return CreateElement(true);
        }

        if(_pools.Count < _maxCapasity)
        {
            return CreateElement(true);
        }

        throw new Exception("Pool is over"); 
    }

    public GameObject GetFreeElement(Vector3 position)
    {
        var element = GetFreeElement();
        element.transform.position = position;
        return element;
    }

    public GameObject GetFreeElement(Vector3 position, Quaternion rotation)
    {
        var element = GetFreeElement(position);
        element.transform.rotation = rotation;
        return element;
    }

    #endregion
}
