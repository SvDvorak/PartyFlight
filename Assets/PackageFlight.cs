using UnityEngine;

public class PackageFlight : MonoBehaviour
{
    public Transform Model;
    public ParticleSystem FallSpread;
    public ParticleSystem CrashSpread;

    public Vector3 Target;
    public float BaseSpeed;
    public float Acceleration;
    private float _currentSpeed;
    private Vector3 _rotate;

    public void Start()
    {
        _currentSpeed = BaseSpeed;
        _rotate = new Vector3(Rot(), Rot(), Rot());
        Model.rotation = Random.rotation;
    }

    private static float Rot()
    {
        return Random.Range(0, 90);
    }

    public void Update()
    {
        var toTarget = Target - transform.position;
        if (toTarget.sqrMagnitude > _currentSpeed*_currentSpeed*4)
        {
            _currentSpeed += Acceleration * Time.deltaTime;
            transform.position += toTarget.normalized * _currentSpeed;
        }
        else
        {
            FallSpread.Stop();
            CrashSpread.Play();
            Destroy(this);
        }

        Model.Rotate(_rotate*Time.deltaTime);
    }
}