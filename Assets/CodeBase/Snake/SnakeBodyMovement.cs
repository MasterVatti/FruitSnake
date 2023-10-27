using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Snake
{
  public class SnakeBodyMovement : MonoBehaviour
  {
    [SerializeField] private float _bodyMovementSpeed;
    [SerializeField] private Transform _snakeTransform;
    [SerializeField] private Transform _snakeRotatingTransform;
    [SerializeField] private List<Transform> _movementBodies;
    [SerializeField] private List<Transform> _rotationBodies;
    [SerializeField] private float _minimumBodyDistance;

    private Transform _currentBodyPart;
    private Transform _currentRotationBodyPart;
    private Transform _previousBodyPart;
    private Transform _previousRotationBodyPart;

    private void FixedUpdate()
    {
      float additionalCoefficient = _movementBodies.Count + 2;
      for (var i = 0; i < _movementBodies.Count; i++)
      {
        SelectBodyPart(i);
        float time = GatSlerpAmountTime(additionalCoefficient);
        ChangeBodyPartLocation(time, i);
        additionalCoefficient -= 0.3f;
      }
    }

    public void AddSnakeBodyPart(GameObject body)
    {
      Transform tail = _rotationBodies[_rotationBodies.Count - 1];
      body.transform.position = tail.position;
      body.transform.rotation = tail.rotation;
      _rotationBodies.Add(body.transform.GetChild(0));
      _movementBodies.Add(body.transform);
    }

    private void SelectBodyPart(int i)
    {
      if (i == 0)
      {
        _previousBodyPart = _snakeTransform;
        _previousRotationBodyPart = _snakeRotatingTransform;
      }
      else
      {
        _previousBodyPart = _movementBodies[i - 1];
        _previousRotationBodyPart = _rotationBodies[i - 1];
      }

      _currentBodyPart = _movementBodies[i];
    }

    private float GatSlerpAmountTime(float additionalCoefficient)
    {
      float distance = Vector3.Distance(_previousBodyPart.position, _currentBodyPart.position);
      float time = Time.deltaTime * distance / _minimumBodyDistance * _bodyMovementSpeed * additionalCoefficient;
      if (time > 0.5f) time = 0.5f;
      return time;
    }

    private void ChangeBodyPartLocation(float time, int i)
    {
      _currentBodyPart.position = Vector3.Slerp(_currentBodyPart.position, _previousBodyPart.position, time);
      _rotationBodies[i].rotation =
        Quaternion.Slerp(_rotationBodies[i].rotation, _previousRotationBodyPart.rotation, time);
    }
  }
}