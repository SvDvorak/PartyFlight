using UnityEngine;

public class FlightControls : MonoBehaviour
{
    public float Speed;

    public void Start()
    {

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
