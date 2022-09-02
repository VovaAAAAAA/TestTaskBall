using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    private Vector3 _positionMouse;
    private Vector3 _startPosLR;
    private Vector3 _endPosLR;
    private Vector3 _moveDir;

    private Rigidbody _rb;
    private Camera _camera;
    private float _powerDirection;
    
    [Header("LineRenderer for ball")]
    [SerializeField] private LineRenderer _lineRenderer;

    [Header("Ignore LayerMask")]
    [SerializeField] private LayerMask _layerMask;

    #region Mono

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _lineRenderer.positionCount = 2;
    }

    #endregion

    #region Callbacks
    private void Update()
    {
        CalculationRoute();

        PushBall();

        _rb.angularVelocity = Vector3.Cross(-_rb.velocity * 1.5f, Vector3.up);
    }

    #endregion

    #region Private Methods
    private void PushBall()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
            if (_powerDirection > 5)
                _powerDirection = 5;

            _rb.velocity = -_moveDir * _powerDirection / 2;
        }
    }

    private void CalculationRoute()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _layerMask))
            {
                _positionMouse = new Vector3(hit.point.x, 1, hit.point.z);

                _moveDir = _positionMouse - this.gameObject.transform.position;
                Vector3 endPosLineRenderer = -_moveDir + this.gameObject.transform.position;

                _powerDirection = (_positionMouse - transform.position).magnitude;

                DrawLine(endPosLineRenderer);
            }
        }
    }

    private void DrawLine(Vector3 endPosLineRenderer)
    {
        _startPosLR = gameObject.transform.position;
        _startPosLR.y = 0;
        _lineRenderer.SetPosition(0, _startPosLR);

        _endPosLR = endPosLineRenderer;
        _endPosLR.y = 0;
        _lineRenderer.SetPosition(1, _endPosLR);
    }
    #endregion
}
