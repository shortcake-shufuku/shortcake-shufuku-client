using UnityEngine;

public class CameraRigControl : MonoBehaviour
{
  public float DampTime = 0.2f;

  private Camera Camera;
  private Vector3 MoveVelocity;

  private Vector3 StartAngles;

  private void Awake()
  {
    Camera = GetComponentInChildren<Camera>();
  }

  public void Init(Transform CameraStartPoint)
  {
    Camera.transform.localPosition = CameraStartPoint.localPosition;
    Camera.transform.eulerAngles = CameraStartPoint.eulerAngles;
    StartAngles = CameraStartPoint.eulerAngles;
  }

  public void Move(Transform DesiredPoint, float Time)
  {
    
    float yAngle = Mathf.LerpAngle(StartAngles.y, DesiredPoint.eulerAngles.y, Time);
    
    Debug.Log("yAngle" + yAngle);

    Camera.transform.eulerAngles = new Vector3(0, yAngle, 0);

    // Camera.transform.position = Vector3.SmoothDamp
    //   (
    //     Camera.transform.position,
    //     DesiredPoint.position,
    //     ref MoveVelocity,
    //     DampTime
    //   );
  }
}