using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider))]  
public class Money : MonoBehaviour
{
    [SerializeField] private int _amountCoins;
    [SerializeField] private GameObject _particleAfterSelection;
    private Animator _animator;

    #region Mono
    private void OnValidate()
    {
        if (_amountCoins < 0)
            _amountCoins *= -1;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    #endregion

    #region Callbacks
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Controller>() != null)
        {
            Bank.AddCoins(_amountCoins);
            _animator.SetBool("DisableCoin", true);
        }
    }
    #endregion

    #region Private Method
    //used at the end of the animation
    private void DestroyMoney()
    {
        Vector3 vector = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Instantiate(_particleAfterSelection, vector, Quaternion.identity);
        Destroy(this.gameObject);
    }
    #endregion
}
