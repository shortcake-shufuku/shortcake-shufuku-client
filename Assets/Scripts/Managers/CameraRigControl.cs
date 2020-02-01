using UnityEngine;

public class CameraRigControl : MonoBehaviour
{
  public float m_DampTime = 0.2f;
  public float m_ScreenEdgeBuffer = 4f;
  public float m_MinSize = 10.5f;
  [HideInInspector] public Transform[] Targets;
  // public Transform[] m_Targets;

  private Camera m_Camera;
  private float m_ZoomSpeed;
  private Vector3 m_MoveVelocity;
  private Vector3 m_DesiredPosition;


  private void Awake()
  {
    m_Camera = GetComponentInChildren<Camera>();
  }

  private void FixedUpdate()
  /* Physics Update */
  {
    Move();
    Zoom();
  }


  private void Move()
  {
    FindAveragePosition();
    transform.position = Vector3.SmoothDamp
      (
        transform.position,
        m_DesiredPosition,
        ref m_MoveVelocity,
        m_DampTime
      );
  }


  private void FindAveragePosition()
  {
    Vector3 averagePos = new Vector3();
    int numTargets = 0;

    foreach (Transform target in Targets)
    {
      if (!target.gameObject.activeSelf)
        // if target object is not active, do not consider it.
        continue;

      // add position to the average position.
      averagePos += target.position;
      // count up the number of targets that there are.
      numTargets++;
    }

    if (numTargets > 0)
      averagePos /= numTargets;

    // for safety's sake. Tanks are attached to y0 plane anyways.
    averagePos.y = transform.position.y;

    // define the desired position as that calculated average position.
    m_DesiredPosition = averagePos;
  }


  private void Zoom()
  {
    float requiredSize = FindRequiredSize();
    m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
  }


  private float FindRequiredSize()
  {
    // acquire the localized position of the desired camera center (the local rig location)
    // this is calculated in FindAveragePosition(), which is resolved earlier.
    Vector3 cameraCenterLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

    float size = 0f;

    foreach (Transform target in Targets)
    {
      if (!target.gameObject.activeSelf)
        // if target object is not active, do not consider it.
        continue;

      // identify where the target is in relation to the camera rig's local vector plane
      Vector3 targetLocalPos = transform.InverseTransformPoint(target.position);

      Vector3 distanceToTarget = targetLocalPos - cameraCenterLocalPos;


      // TODO: consolidate into one line
      size = Mathf.Max(size, Mathf.Abs(distanceToTarget.y));
      size = Mathf.Max(size, Mathf.Abs(distanceToTarget.x) / m_Camera.aspect);
    }

    // add extra bit of distance.
    size += m_ScreenEdgeBuffer;

    // make sure camera is not too zoomed in.
    size = Mathf.Max(size, m_MinSize);

    return size;
  }


  public void Reset()
  /* for use by GameManager */
  {
    FindAveragePosition();

    transform.position = m_DesiredPosition;

    m_Camera.orthographicSize = FindRequiredSize();
  }
}