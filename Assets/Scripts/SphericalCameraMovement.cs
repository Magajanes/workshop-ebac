using UnityEngine;

public class SphericalCameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothness;
    [SerializeField]
    private float mouseSensitivity;

    private Vector3 cameraRadius;
    private Vector3 nextPosition;

    private float fixedRadius;
    private float theta = 1;
    private float phi = 3.89f;

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
        cameraRadius = transform.position - target.position;
        fixedRadius = cameraRadius.magnitude;
    }

    private void SetParametersValues()
    {
        theta += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        theta = Mathf.Clamp(theta, 0.01f, Mathf.PI / 2);
        phi -= Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    }

    private void SetParametricPosition()
    {
        cameraRadius.x = fixedRadius * Mathf.Sin(theta) * Mathf.Cos(phi);
        cameraRadius.y = fixedRadius * Mathf.Cos(theta);
        cameraRadius.z = fixedRadius * Mathf.Sin(theta) * Mathf.Sin(phi);
    }

    private void SetCameraPosition()
    {
        nextPosition = target.position + cameraRadius;
        transform.position = Vector3.Lerp(transform.position, nextPosition, smoothness * Time.deltaTime);
        transform.forward = (target.position - transform.position).normalized;
    }
}
