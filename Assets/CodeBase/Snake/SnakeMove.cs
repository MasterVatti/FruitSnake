using System;
using Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Snake
{
  public class SnakeMove : MonoBehaviour
  {
    public bool IsDied { get; set; }

    // [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Rigidbody _snakeRigidbody;
    [SerializeField] private Transform _snakeTransform;

    private IInputService _inputService;
    private Camera _camera;
    private GameObject _appleObject;
    private Vector3 _groundNormal;

    public void Constructor(IInputService inputService)
    {
      Debug.Log($"Constructor {inputService}");
      _inputService = inputService;
    }

    private void Awake()
    {
      _appleObject = GameObject.FindWithTag("Apple");
    }

    private void Start() =>
      _camera = Camera.main;

    private void FixedUpdate()
    {
      Vector3 localUp = transform.up;
      if (UnityEngine.Input.GetKey(KeyCode.E))
      {
        transform.Rotate(0, 150 * Time.deltaTime, 0);
      }
      if (UnityEngine.Input.GetKey(KeyCode.Q))
      {
        transform.Rotate(0, 150 * Time.deltaTime, 0);
      }
      RaycastHit hit = new RaycastHit();
      if (Physics.Raycast(transform.position, -localUp, out hit, 10))
      {
        _groundNormal = hit.normal;
      }
      
      // _snakeRigidbody.MovePosition(_snakeRigidbody.position + transform.TransformDirection(_inputService.Axis * _movementSpeed * Time.deltaTime));
      _snakeRigidbody.MovePosition(_snakeRigidbody.position +transform.TransformDirection(_inputService.Axis * _movementSpeed * Time.deltaTime));
      Vector3 gravityUp = (transform.position - _appleObject.transform.position).normalized;
      
      _snakeRigidbody.AddForce(gravityUp * -12);
      // Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
      // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50f * Time.deltaTime);
      // transform.up = Vector3.Lerp(transform.up, gravityUp, 20 * Time.deltaTime);

      Quaternion toRotation = Quaternion.FromToRotation(localUp, _groundNormal) * transform.rotation;
      transform.rotation = toRotation;
    }
  }
}