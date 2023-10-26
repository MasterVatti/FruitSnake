using System.Collections.Generic;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Snake
{
  public class SnakeBodyMovement : MonoBehaviour
  {
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Rigidbody _snakeRigidbody;
    [SerializeField] private Transform _snakeTransform;
    [SerializeField] private Transform _snakeRotatingTransform;
    [SerializeField] private float _bonesDistance;
    [SerializeField] private GameObject _bonePrefab;
    [SerializeField] private List<Transform> _tails;
    [SerializeField] private List<Transform> _colliderTails;
    [SerializeField] private float _speedRotate = 5f;
    [SerializeField] private float _mindistance;
    [SerializeField] private float _f = 0.5f;
    [SerializeField] private float _f1 = 0.5f;

    private IInputService _inputService;
    private Camera _camera;
    private GameObject _appleObject;
    private GameObject _joystick;
    private Vector3 _groundNormal;
    private Transform _curBodyPart;
    private Transform _curRotationBodyPart;
    private Transform _prevBodyPart;
    private Transform _prevRotationBodyPart;

    public void AddSnakeBody(GameObject body)
    {
      Transform tail = _colliderTails[_colliderTails.Count - 1];
      body.transform.position = tail.position;
      body.transform.rotation = tail.rotation;
      _colliderTails.Add(body.transform.GetChild(0));
      _tails.Add(body.transform);
    }

    public void Constructor(IInputService inputService)
    {
      Debug.Log($"Constructor {inputService}");
      _inputService = inputService;
    }

    private void Awake()
    {
      _appleObject = GameObject.FindWithTag("Apple");
      _joystick = GameObject.FindWithTag("Joystick");
    }

    private void Start() =>
      _camera = Camera.main;
    
    private void FixedUpdate()
    {
      
      float j = _tails.Count + 2;
      for (var i = 0; i < _tails.Count; i++)
      {
        if (i == 0)
        {
          _prevBodyPart = _snakeTransform;
          _prevRotationBodyPart = _snakeRotatingTransform;
        }
        else
        {
          _prevBodyPart = _tails[i - 1];
          _prevRotationBodyPart = _colliderTails[i - 1];
        }
        _curBodyPart = _tails[i];

        var dis = Vector3.Distance(_prevBodyPart.position, _curBodyPart.position);
        Vector3 newpos = _prevBodyPart.position;
        // newpos.y = _tails[0].position.y;
        float T = Time.deltaTime * dis / _mindistance * _movementSpeed * j;
        if (T > _f) T = _f1;

        _curBodyPart.position = Vector3.Slerp(_curBodyPart.position, newpos, T);
        // _curBodyPart.rotation = Quaternion.Slerp(_curBodyPart.rotation, _prevBodyPart.rotation, T);
        
        
        _colliderTails[i].rotation = Quaternion.Slerp(_colliderTails[i].rotation, _prevRotationBodyPart.rotation, T);
        j -= 0.3f;
      }
    }
  }
}