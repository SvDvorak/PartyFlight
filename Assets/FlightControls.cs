using UnityEngine;

public class FlightControls : MonoBehaviour
{
    public float Speed;
    public GameObject TargetReticule;
    public GameObject PackageTemplate;
    public Transform DropPoint;
    public Transform CameraPosition;
    public float RotateStrength;

    public float MultiplierChange;
    private int _speedMultiplier;
    private Vector3 _cameraPositionInitial;
    private GameObject _packageRoot;

    public void Start()
    {
        _cameraPositionInitial = CameraPosition.transform.localPosition;
        _packageRoot = new GameObject("PackageRoot");
    }

    public void Update()
    {
        RaycastHit hitInfo;
        var planeYRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        var ray = new Ray(transform.position, planeYRotation * new Vector3(0, -1, 1));
        var hasHitGround = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground"));
        if (hasHitGround)
        {
            TargetReticule.transform.position = hitInfo.point;
        }

        if (Input.GetButtonDown("Drop"))
        {
            var package = Instantiate(PackageTemplate, DropPoint.position, Quaternion.identity, _packageRoot.transform);
            package.GetComponent<PackageFlight>().Target = TargetReticule.transform.position;
            Debug.Log("Dropped");
        }

        if (Input.GetButtonDown("RevUp"))
        {
            _speedMultiplier = Mathf.Clamp(_speedMultiplier + 1, 1, 4);
        }

        if (Input.GetButtonDown("RevDown"))
        {
            _speedMultiplier = Mathf.Clamp(_speedMultiplier - 1, 1, 4);
        }
    }

    public void FixedUpdate()
    {
        var pitch = Input.GetAxis("Pitch");
        var yaw = Input.GetAxis("Yaw") * 0.5f;
        var roll = Input.GetAxis("Roll");
        RotateStrength = Mathf.Abs(pitch) + Mathf.Abs(yaw) + Mathf.Abs(roll);
        transform.Rotate(pitch, yaw, roll);

        transform.Translate(0, 0, Speed*_speedMultiplier*MultiplierChange, Space.Self);

        CameraPosition.transform.localPosition = _cameraPositionInitial - new Vector3(0, pitch*3, 0);
    }
}
