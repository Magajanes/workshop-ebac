using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public const float RESET_THRESHOLD = 0.2f;

    [SerializeField]
    private GameObject _playerObject;
    [SerializeField]
    private float _cameraLerpSpeed;

    private Vector3 _initialPlayerPosition;

    private void Awake()
    {
        GroundDetector.OnPlayerDestroyed -= ResetPlayer;
        GroundDetector.OnPlayerDestroyed += ResetPlayer;
    }

    private void Start()
    {
        _initialPlayerPosition = _playerObject.transform.position;
    }

    private void ResetPlayer()
    {
        StartCoroutine(PlayerResetCoroutine());
    }

    private IEnumerator PlayerResetCoroutine()
    {
        _playerObject.SetActive(false);

        while (Vector3.Distance(_playerObject.transform.position, _initialPlayerPosition) > RESET_THRESHOLD)
        {
            _playerObject.transform.position = Vector3.Lerp(_playerObject.transform.position, _initialPlayerPosition, _cameraLerpSpeed * Time.deltaTime);
            yield return null;
        }

        _playerObject.transform.position = _initialPlayerPosition;
        _playerObject.SetActive(true);
    }
}
