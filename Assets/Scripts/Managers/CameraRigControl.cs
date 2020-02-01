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

  private void FixedUpdate()
  /* Physics Update */
  {
      
  }


  public void Move(Transform DesiredTarget)
  {
    transform.position = Vector3.SmoothDamp
      (
        transform.position,
        DesiredTarget.position,
        ref MoveVelocity,
        DampTime
      );
  }

  public void Reset(Transform ResetTarget)
  /* for use by GameManager */
  {
    transform.position = ResetTarget.position;
  }
}