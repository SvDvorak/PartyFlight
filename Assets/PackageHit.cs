using UnityEngine;

public class PackageHit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Package")
        {
            transform.parent.SendMessage("PackageCollided");
        }
    }
}
