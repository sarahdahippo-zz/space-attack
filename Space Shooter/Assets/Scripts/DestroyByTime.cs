using UnityEngine;
using System.Collections;
public class DestroyByTime : MonoBehaviour
{
    public float lifetime;
    void Start()
    {
        // Destroy(Object, time to live)
        // Destroys the game object after a set amount of seconds
        Destroy(gameObject, lifetime);
    }
}