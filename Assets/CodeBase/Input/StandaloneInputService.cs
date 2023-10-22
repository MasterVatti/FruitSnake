// using UnityEngine;
// using Zenject;
//
// namespace Input
// {
//   public class StandaloneInputService : InputService, IInitializable
//   {
//     public override Vector2 Axis
//     {
//       get
//       {
//         Vector2 axis = SimpleInputAxis();
//
//         if (axis == Vector2.zero)
//         {
//           axis = UnityAxis();
//         }
//
//         return axis;
//       }
//     }
//
//     private static Vector2 UnityAxis()
//     {
//       return new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
//     }
//
//     public void Initialize()
//     {
//       
//     }
//   }
// }