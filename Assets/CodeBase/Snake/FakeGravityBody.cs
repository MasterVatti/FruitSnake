using UnityEngine;

namespace CodeBase.Snake
{
  [RequireComponent(typeof(Rigidbody))]
  public class FakeGravityBody : MonoBehaviour
  {
    private Transform _objTransform;
    private FakeGravity _attractor;

    private void Awake()
    {
      _attractor = GameObject.FindGameObjectWithTag("Planet").GetComponent<FakeGravity>();
    }

    private void Start()
    {
      _objTransform = transform;
    }
    
    private void Update()
    {
      _attractor.Attract(_objTransform);
    }
  }
}