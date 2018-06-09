using UnityEngine;

public class PackageFlight : MonoBehaviour
{
    public Vector3 Target;
    public float BaseSpeed;
    public float Acceleration;
    private float _currentSpeed;

    public void Start()
    {
        _currentSpeed = BaseSpeed;
    }

    public void Update()
    {
        var toTarget = Target - transform.position;
        if (toTarget.sqrMagnitude > _currentSpeed*_currentSpeed)
        {
            _currentSpeed += Acceleration * Time.deltaTime;
            transform.position += toTarget.normalized * _currentSpeed;
        }
        else
        {
            Destroy(this);
        }
    }
}