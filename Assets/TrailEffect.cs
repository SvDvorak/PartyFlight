using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    public FlightControls FlightControls;
    private readonly List<TrailRenderer> _trails = new List<TrailRenderer>();
    private float _data = 0;

    public void Start()
    {
        foreach (Transform child in transform)
        {
            _trails.Add(child.GetComponent<TrailRenderer>());
        }
    }

    public void Update()
    {
        var strength = FlightControls.RotateStrength;
        if (strength > 0.1)
            _data = (_data + strength * Time.fixedDeltaTime);
        else
            _data *= 0.9f;

        var adjusted = Mathf.Clamp01(_data - 0.5f);

        foreach (var trail in _trails)
        {
            trail.startWidth = adjusted*0.1f;
            trail.endWidth = adjusted*0.1f;
        }
    }
}
