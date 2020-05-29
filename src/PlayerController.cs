using UnityEngine;
using System.Collections;

// Type the following for the Boundary parameter field to be shown in Unity


public class PlayerController : MonoBehaviour
{
    // These will appear in Unity as parameters
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    
    // This is the behaviour that runs when fire is triggered
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;    // Prevents spamming shots
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play(); // Adds audio when firing
        }
    }
    
    void FixedUpdate()
    {
        // This takes in input from the player
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);     // Determines which axes to move
        GetComponent<Rigidbody>().velocity = movement * speed;

        
         // This prevents the player from leaving our game area!
         // Clamp() set a min and max value for the given value 
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );
        
        
        // This causes the player to tilt while moving
        // -tilt to tilt in the direction we want
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
        
    }
}