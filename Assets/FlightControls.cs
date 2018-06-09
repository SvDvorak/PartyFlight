using UnityEngine;

public class FlightControls : MonoBehaviour
{
    public float Speed;
    public GameObject TargetReticule;
    public GameObject PackageTemplate;
    public Transform DropPoint;

    public void Start()
    {

    }

    public void Update()
    {
        RaycastHit hitInfo;
        var ray = new Ray(transform.position, transform.rotation * new Vector3(0, -1, 1));
        Debug.DrawRay(ray.origin, ray.direction*2, Color.red);
        var hasHitGround = Physics.Raycast(ray, out hitInfo);
        if (hasHitGround)
        {
            TargetReticule.transform.position = hitInfo.point;
        }

        if (Input.GetButtonDown("Drop"))
        {
            var package = Instantiate(PackageTemplate, DropPoint.position, Random.rotation, transform.parent);
            package.GetComponent<PackageFlight>().Target = TargetReticule.transform.position;
            Debug.Log("Dropped");
        }
    }

    public void FixedUpdate()
    {
        var pitch = Input.GetAxis("Pitch");
        var yaw = Input.GetAxis("Yaw") * 0.5f;
        var roll = Input.GetAxis("Roll");
        transform.Rotate(pitch, yaw, roll);

        transform.Translate(0, 0, Speed, Space.Self);
    }
}
