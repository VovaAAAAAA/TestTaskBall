using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private Pool _poolDust;
    private Controller _controller;

    #region Mono
    private void Start()
    {
        _controller = GetComponent<Controller>();
    }
    #endregion

    #region Callbacks
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            _poolDust.GetFreeElement(collision.contacts[0].point, Quaternion.identity);

            _controller.MoveDir = Vector3.Reflect(_controller.MoveDir, -collision.contacts[0].normal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Money money))
        {
            money.SelectionCoins();
        }
    }
    #endregion
}
