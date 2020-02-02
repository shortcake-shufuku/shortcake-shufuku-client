using UnityEngine;

public class CameraRigControl : MonoBehaviour
{
  public float DampTime = 0.2f;

  private Camera Camera;
  private Vector3 MoveVelocity;


  private void Awake()
  {
    Camera = GetComponentInChildren<Camera>();
  }

  private void Move(Transform DesiredPoint)
  {
    transform.position = Vector3.SmoothDamp
      (
        transform.position,
        DesiredPoint.position,
        ref MoveVelocity,
        DampTime
      );

    transform.Rotate(90.0f, 0.0f, 0.0f);
  }


  public void Reset(Transform DesiredPoint)
  /* for use by GameManager */
  {
    transform.position = DesiredPoint.position;
  }
}