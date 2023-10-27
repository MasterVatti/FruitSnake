using UnityEngine;

namespace CodeBase.CameraLogic
{
  public class CameraFollow : MonoBehaviour
  {
    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private bool _lookAt;

    private Transform _snakeTransform;
    private Transform _mainCamera;

    private void Start()
    {
      _mainCamera = Camera.main.transform;
    }

    public void Follow(GameObject following) => _snakeTransform = following.transform;

    private void LateUpdate()
    {
      UpdateCamera();
    }

    private void UpdateCamera()
    {
      transform.position = _snakeTransform.position + -(_snakeTransform.forward * _offsetPosition.z) +
                           (_snakeTransform.up * _offsetPosition.y);

      if (_lookAt) _mainCamera.LookAt(_snakeTransform, _snakeTransform.up);
      else _mainCamera.LookAt(_snakeTransform);
    }
  }
}