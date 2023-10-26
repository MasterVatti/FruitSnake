using UnityEngine;

namespace CodeBase.Snake
{
  public class FakeGravity : MonoBehaviour
  {
    [SerializeField] private float _gravity = -10;

    [SerializeField]private float _rotationSpeed = 50;
    [SerializeField]private float _gravityBoost = 0;

    public void Attract(Transform targetObject)
    {
      Vector3 gravityDirection = (targetObject.position - transform.position).normalized;
      Vector3 targetObjectUp = targetObject.up;
      targetObject.GetComponent<Rigidbody>().AddForce(gravityDirection * (_gravity + _gravityBoost));
      var rotation = targetObject.rotation;
      Quaternion targetRotation = Quaternion.FromToRotation(targetObjectUp, gravityDirection) * rotation;
      rotation = Quaternion.Slerp(rotation, targetRotation, _rotationSpeed * Time.deltaTime);
      targetObject.rotation = rotation;
    }
  }
}