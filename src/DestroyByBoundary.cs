using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        // Destroy gameObjects after it stops being on contact with the boundary 
        Destroy(other.gameObject);
    }
}
