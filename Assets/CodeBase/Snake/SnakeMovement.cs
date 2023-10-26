using System;
using System.Collections;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Snake
{
  [RequireComponent(typeof(Rigidbody))]
  public class SnakeMovement : MonoBehaviour
  {
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float _speedRotate = 5f;

    private Vector3 _moveDirection;
    private Rigidbody _playerRB;
    private Transform _playerMesh;
    private IInputService _inputService;
    private float _angle = 90f;
    private float _previousAngle;

    public void Constructor(IInputService inputService)
    {
      _inputService = inputService;
    }

    private void Awake()
    {
      _moveDirection = _moveDirection = new Vector3(1, 0, 0);
    }

    private void Start()
    {
      _playerRB = GetComponent<Rigidbody>();
      _playerMesh = transform.GetChild(0).transform;
      // _playerMesh = transform;
    }
    
    
    private void FixedUpdate()
    {
      
      if (_inputService.Axis != Vector3.zero)
      {
        _angle = Mathf.Atan2(_inputService.Axis.x, _inputService.Axis.z) * Mathf.Rad2Deg;
        if (_angle is 0f or 90f or 180f or 270f or 360f or -90f or -180f or -270f or -360f)
        {
          Debug.Log($"{_angle} – angle");
          _angle = _previousAngle;
        }
        else
        {
          _previousAngle = _angle;
          _moveDirection = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.z).normalized;
        }
        

      }
      
      
      _playerRB.MovePosition(_playerRB.position + transform.TransformDirection(_moveDirection * speed * Time.deltaTime));
      
      
      RotateForward();
      // _playerRB.velocity = new Vector3(_inputService.Axis.x * speed, _playerRB.velocity.y, _inputService.Axis.z * speed);
    }

    private void RotateForward()
    {
      Vector3 dir = _moveDirection;
      // calculate angle and rotation  * _speedRotate * Time.deltaTime
      float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
      
      Quaternion targetRotation = Quaternion.AngleAxis(_angle, Vector3.up);
      // only update rotation if direction greater than zero

      if (Vector3.Magnitude(dir) > 0.0f)
      {
        _playerMesh.localRotation = targetRotation;

      }
    }
  }
}