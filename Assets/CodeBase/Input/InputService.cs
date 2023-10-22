using UnityEngine;

namespace Input
{
  public class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string Button = "Fire";

// #if UNITY_IOS || UNITY_ANDROID
//     public Vector2 Axis => SimpleInputAxis();
// #else

    public InputService()
    {
      Debug.Log("InputService");
    }
    public Vector3 Axis
    {
      get
      {
        Vector3 axis = SimpleInputAxis();

        if (axis == Vector3.zero)
        {
          axis = UnityAxis();
        }

        return axis;
      }
    }

    private static Vector3 UnityAxis()
    {
      return new Vector3(UnityEngine.Input.GetAxis(Horizontal), 0, UnityEngine.Input.GetAxis(Vertical)).normalized;
    }
// #endif
    

    public bool IsAttackButtonUp()
    {
      return SimpleInput.GetButtonUp(Button);
    }

    private static Vector3 SimpleInputAxis()
    {
      return new Vector3(SimpleInput.GetAxis(Horizontal), 0, SimpleInput.GetAxis(Vertical)).normalized;
    }
  }
}