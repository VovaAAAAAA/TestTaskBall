using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private Pool _poolDust;

    #region Callbacks
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            _poolDust.GetFreeElement(collision.contacts[0].point, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Money>(out Money money))
        {
            money.SelectionCoins();
        }
    }
    #endregion
}
