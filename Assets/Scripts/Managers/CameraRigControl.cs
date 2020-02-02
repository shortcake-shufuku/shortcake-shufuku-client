using UnityEngine;

public class CameraRigControl : MonoBehaviour
{
  public float DampTime = 0.2f;

  private Camera FirstPersonCamera;
  public Camera TopDownCamera;
  private Vector3 MoveVelocity;

  private Vector3 StartAngles;

  private void Awake()
  {
    TopDownCamera = transform.Find("TopDownCamera").gameObject.GetComponent<Camera>();
    TopDownCamera.gameObject.SetActive(false);
    FirstPersonCamera = transform.Find("FirstPersonCamera").gameObject.GetComponent<Camera>();
  }

  public void Init(Transform CameraStartPoint)
  {
    FirstPersonCamera.transform.localPosition = CameraStartPoint.localPosition;
    FirstPersonCamera.transform.eulerAngles = CameraStartPoint.eulerAngles;
    StartAngles = CameraStartPoint.eulerAngles;
  }

  public void RoundStart()
  {
    FirstPersonCamera.gameObject.SetActive(false);
    TopDownCamera.gameObject.SetActive(true);
  }

  public void Move(Transform DesiredPoint, float Time)
  {
    
    float yAngle = Mathf.LerpAngle(StartAngles.y, DesiredPoint.eulerAngles.y, Time);
    
    FirstPersonCamera.transform.eulerAngles = new Vector3(0, yAngle, 0);

    // FirstPersonCamera.transform.position = Vector3.SmoothDamp
    //   (
    //     FirstPersonCamera.transform.position,
    //     DesiredPoint.position,
    //     ref MoveVelocity,
    //     DampTime
    //   );
  }
}