using UnityEngine;

public class SphericalCameraMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform target;

    [Header("Parameters")]
    [SerializeField]
    private float smoothness;
    [SerializeField]
    private float mouseSensitivity;
    [SerializeField]
    private float zoomSensitivity;

    private Vector3 _cameraRadius;
    private Vector3 _nextPosition;
    private Vector3 _cameraForward;

    private float _radius;
    private float _theta = 1;
    private float _phi = 3.89f;

    public Vector3 Forward
    {
        get
        {
            _cameraForward = transform.forward;
            _cameraForward.y = 0;
            return _cameraForward.normalized;
        }
    }

    public Vector3 Right => transform.right;

    private void Start()
    {
        SetInitialValues();
        SetParametricPosition();
    }

    private void Update()
    {
        SetParametersValues();
        SetParametricPosition();
    }

    private void LateUpdate()
    {
        SetCameraPosition();
    }

    private void SetInitialValues()
    {
        _cameraRadius = transform.position - target.position;
        _radius = _cameraRadius.magnitude;
    }

    private void SetParametersValues()
    {
        _radius -= Input.mouseScrollDelta.y * zoomSensitivity * Time.deltaTime;
        
        if (!Input.GetMouseButton(1)) return;
        
        _theta += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        _theta = Mathf.Clamp(_theta, 0.2f, Mathf.PI / 2);
        _phi -= Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    }

    private void SetParametricPosition()
    {
        _cameraRadius.x = _radius * Mathf.Sin(_theta) * Mathf.Cos(_phi);
        _cameraRadius.y = _radius * Mathf.Cos(_theta);
        _cameraRadius.z = _radius * Mathf.Sin(_theta) * Mathf.Sin(_phi);
    }

    private void SetCameraPosition()
    {
        _nextPosition = target.position + _cameraRadius;
        transform.position = Vector3.Lerp(transform.position, _nextPosition, smoothness * Time.deltaTime);
        transform.forward = (target.position - transform.position).normalized;
    }
}
