using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForPlayer : MonoBehaviour
{
    [Header("Object for following")]
    [SerializeField] private GameObject _mainCharacter;

    [Header("Camera propertys")]
    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _height;
    [SerializeField] private float _rearDistance;

    private Vector3 _currentVector;

    #region Mono
    private void Start()
    {
        transform.position = new Vector3(_mainCharacter.transform.position.x, _mainCharacter.transform.position.y + _height, _mainCharacter.transform.position.z - _rearDistance);
    }
    #endregion

    #region CallBacks

    private void Update()
    {
        CameraMove();
    }

    #endregion

    #region Private Method

    private void CameraMove()
    {
        _currentVector = new Vector3(_mainCharacter.transform.position.x, _mainCharacter.transform.position.y + _height, _mainCharacter.transform.position.z - _rearDistance);
        transform.position = Vector3.Lerp(transform.position, _currentVector, _returnSpeed * Time.deltaTime);
    }

    #endregion
}
