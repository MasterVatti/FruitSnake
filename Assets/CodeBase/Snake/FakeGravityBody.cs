using UnityEngine;

namespace CodeBase.Snake
{
  [RequireComponent(typeof(Rigidbody))]
  public class FakeGravityBody : MonoBehaviour
  {
    private Transform _targetTransform;
    private FakeGravity _attractor;

    private void Awake()
    {
      _attractor = GameObject.FindGameObjectWithTag("Ground").GetComponent<FakeGravity>();
    }

    private void Start()
    {
      _targetTransform = transform;
    }
    
    private void Update()
    {
      _attractor.Attract(_targetTransform);
    }
  }
}