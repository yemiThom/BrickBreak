using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    public float ballInitVelocity = 600f;

    private Rigidbody rbody;
    private bool ballInPlay;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !ballInPlay){
            transform.parent = null;
            ballInPlay = true;
            rbody.isKinematic = false;
            rbody.AddForce(new Vector3(ballInitVelocity, ballInitVelocity, 0));
        }
    }
}
