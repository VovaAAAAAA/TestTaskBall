using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private GameObject _dustParticle;

    #region Callbacks
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Instantiate(_dustParticle, collision.contacts[0].point, Quaternion.identity);
        }
    }
    #endregion
}
