using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CheckCollision))]
public class Controller : MonoBehaviour
{
    private Vector3 _positionMouse;
    private Vector3 _startPosLR;
    private Vector3 _endPosLR;
    private Vector3 _moveDir;
    public Vector3 MoveDir 
    { 
        get { return _moveDir; } set {_moveDir = Vector3.ClampMagnitude(value, 12); } 
    }

    private Camera _camera;
    private float _powerDirection;
    
    [Header("LineRenderer for ball")]
    [SerializeField] private LineRenderer _lineRenderer;

    [Header("Ignore LayerMask")]
    [SerializeField] private LayerMask _layerMask;

    #region Mono

    private void Start()
    {
        _camera = Camera.main;
        _lineRenderer.positionCount = 2;
    }

    #endregion

    #region Callbacks
    private void Update()
    {
        RotateBall();

        CalculationRoute();

        PushBall();

    }

    #endregion

    #region Private Methods
    private void PushBall()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }

        _moveDir.y = 0;

        if (_powerDirection > 0)
        {
            _powerDirection -= Time.fixedDeltaTime;
            transform.Translate(-_moveDir * _powerDirection * Time.deltaTime, Space.World);

        }
        else
        {
            _powerDirection = 0;
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
                _positionMouse = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                Vector3 moveDir = _positionMouse - this.gameObject.transform.position;
                Vector3 endPosLineRenderer = -moveDir + this.gameObject.transform.position;


                DrawLine(endPosLineRenderer);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _moveDir = _positionMouse - this.gameObject.transform.position;
            _powerDirection = (_positionMouse - transform.position).magnitude;

            _moveDir = Vector3.ClampMagnitude(_moveDir, 12);
            _powerDirection = Mathf.Clamp(_powerDirection, 0, 3);
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

    private void RotateBall()
    {
        if (_powerDirection > 0)
            transform.RotateAround(transform.position, Vector3.Cross(_moveDir, Vector3.up), _powerDirection);
    }
    #endregion
}
