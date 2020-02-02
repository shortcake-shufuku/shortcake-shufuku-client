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

  public void Init(Transform CameraStartPoint)
  {
    Camera.transform.localPosition = CameraStartPoint.localPosition;
    Camera.transform.eulerAngles = CameraStartPoint.rotation.eulerAngles;
  }

  private void Move(Transform DesiredPoint)
  {
    Camera.transform.position = Vector3.SmoothDamp
      (
        Camera.transform.position,
        DesiredPoint.position,
        ref MoveVelocity,
        DampTime
      );

    Camera.transform.Rotate(90.0f, 0.0f, 0.0f);
  }
}