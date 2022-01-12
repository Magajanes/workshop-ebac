using UnityEngine;

public class SphericalCameraMovement : MonoBehaviour
{
    public const float RADIUS_LOWER_BOUND = 2.2f;
    public const float RADIUS_UPPER_BOUND = 8f;

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
    [SerializeField]
    private float lookSmoothness;
    [HideInInspector]
    public bool FollowHeight;

    private Vector3 _cameraRadius;
    private Vector3 _cameraForward;

    private float _radius;
    private float _theta = 1;
    private float _phi = 3.89f;
    private readonly Vector3 _offset = 1.6f * Vector3.up;

    private float _rigidLookHeight;
    private float _rigidCameraHeight;

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
        FollowHeight = true;
    }

    private void SetParametersValues()
    {
        _radius -= Input.mouseScrollDelta.y * zoomSensitivity * Time.deltaTime;
        _radius = Mathf.Clamp(_radius, RADIUS_LOWER_BOUND, RADIUS_UPPER_BOUND);

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
        Vector3 nextPosition = target.position + _cameraRadius;
        nextPosition.y = FollowHeight ? nextPosition.y : _rigidCameraHeight;
        transform.position = Vector3.Lerp(transform.position, nextPosition, smoothness * Time.deltaTime);

        Vector3 lookPosition = target.position;
        lookPosition.y = FollowHeight ? lookPosition.y : _rigidLookHeight;
        transform.forward = Vector3.Lerp(transform.forward, (lookPosition + _offset - transform.position).normalized, lookSmoothness * Time.deltaTime);
    }

    public void SetRigidLookHeight(float height)
    {
        _rigidLookHeight = height;
        _rigidCameraHeight = transform.position.y;
    }
}
