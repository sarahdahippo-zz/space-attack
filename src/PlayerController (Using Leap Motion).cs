using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class LEAPCtrl : MonoBehaviour
{

    Controller controller;
    Hand firstHand;
    //variables to be used to create gesture
    Finger thumbL;
    Finger indexL;
    Finger middleL;
    Finger ringL;
    Finger pinkyL;

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;


    void Start()
    {

    }


    void Update()
    {
        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;

        
        if (frame.Hands.Count > 0)
        {
            firstHand = hands[0];
            thumbL = firstHand.Fingers[0];
            pinkyL = firstHand.Fingers[4];


            if (thumbL.IsExtended && pinkyL.IsExtended && Time.time > nextFire) {
                nextFire = Time.time + fireRate;    // Prevents spamming shots
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play(); // Adds audio when firing
            }

            Vector3 palmNormal = UnityVectorExtension.ToVector3(firstHand.PalmNormal); //using palm rotation
            Vector3 movement = new Vector3(-palmNormal[0], 0.0f, palmNormal[2]);

            //Vector3 palmPos = UnityVectorExtension.ToVector3(firstHand.PalmPosition); //using hand position
            //Vector3 movement = new Vector3(palmPos[0] * 0.01f , 0.0f, -palmPos[2] * 0.01f);

            GetComponent<Rigidbody>().velocity = movement * speed;
        }


        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}

