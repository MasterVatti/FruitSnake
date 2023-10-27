using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Snake
{
  [RequireComponent(typeof(Rigidbody))]
  public class SnakeMovement : MonoBehaviour
  {
    [SerializeField] private float _headMovementSpeed;
    [SerializeField] private Rigidbody _snakeRigidbody;
    [SerializeField] private Transform _childRotatingTransform;

    private IInputService _inputService;
    private Vector3 _moveDirection = new(1, 0, 0);
    private float _movementAngle = 90f;
    private float _previousMovementAngle;

    private void FixedUpdate()
    {
      SetMoveDirection();
      _snakeRigidbody.MovePosition(_snakeRigidbody.position +
                                   transform.TransformDirection(_moveDirection * _headMovementSpeed * Time.deltaTime));
      RotateForward();
    }

    public void Constructor(IInputService inputService)
    {
      _inputService = inputService;
    }

    private void SetMoveDirection()
    {
      if (_inputService.Axis != Vector3.zero)
      {
        _movementAngle = Mathf.Atan2(_inputService.Axis.x, _inputService.Axis.z) * Mathf.Rad2Deg;
        // specific joystick plugin, need to other joystick
        if (_movementAngle is 0f or 90f or 180f or 270f or 360f or -90f or -180f or -270f or -360f)
        {
          _movementAngle = _previousMovementAngle;
        }
        else
        {
          _previousMovementAngle = _movementAngle;
          _moveDirection = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.z).normalized;
        }
      }
    }

    private void RotateForward()
    {
      Quaternion targetRotation = Quaternion.AngleAxis(_movementAngle, Vector3.up);
      if (Vector3.Magnitude(_moveDirection) > 0.0f) _childRotatingTransform.localRotation = targetRotation;
    }
  }
}