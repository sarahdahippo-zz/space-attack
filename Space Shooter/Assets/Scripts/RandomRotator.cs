using UnityEngine;
using System.Collections;
public class RandomRotator : MonoBehaviour
{
    public float tumble;
    void Start()
    {
        // Angular Velocity determines how fast an object is rotating
        // We use a random generated Vector3 value to determine our angular velocity
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
}