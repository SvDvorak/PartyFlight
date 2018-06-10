using UnityEngine;

[ExecuteInEditMode]
public class TargetMarker : MonoBehaviour
{
    [Range(1, 40)]
    public float Size = 2;
    public BoxCollider Collider;

    private Transform[] _corners;
    private float _sizeAnimation = 1;

    public void Start()
    {
        _corners = new Transform[4];
        var i = 0;
        foreach (Transform corner in transform)
        {
            if (corner.name.Contains("Corner"))
            {
                _corners[i] = corner;
                i++;
            }
        }
    }

    public void Update()
    {
        _sizeAnimation = 1 + Mathf.Sin(Time.time * 2) * 0.1f;
        if (_corners != null)
        {
            PlaceCorner(_corners[0], Vector3.forward + Vector3.right, 180);
            PlaceCorner(_corners[1], Vector3.back + Vector3.right, 270);
            PlaceCorner(_corners[2], Vector3.back + Vector3.left, 0);
            PlaceCorner(_corners[3], Vector3.forward + Vector3.left, 90);
        }

        Collider.size = new Vector3(Size + 0.1f, 2, Size + 0.1f) * 2f;

        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hitInfo))
        {
            var forward = Vector3.Cross(transform.right, hitInfo.normal);
            transform.rotation = Quaternion.LookRotation(forward);
        }
    }

    private void PlaceCorner(Transform corner, Vector3 direction, int rotation)
    {
        corner.localPosition = direction * Size * _sizeAnimation;
        corner.localRotation = Quaternion.Euler(0, rotation, 0);
    }
}
