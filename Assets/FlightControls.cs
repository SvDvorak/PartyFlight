﻿using UnityEngine;

public class FlightControls : MonoBehaviour
{
    public float Speed;
    public GameObject TargetReticule;
    public GameObject PackageTemplate;
    public Transform DropPoint;
    public float RotateStrength;

    private int _speedMultiplier;
    public float _multiplierChange;

    public void Start()
    {

    }

    public void Update()
    {
        RaycastHit hitInfo;
        var planeYRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        var ray = new Ray(transform.position, planeYRotation * new Vector3(0, -1, 1));
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

        transform.Translate(0, 0, Speed*_speedMultiplier*_multiplierChange, Space.Self);
    }
}
