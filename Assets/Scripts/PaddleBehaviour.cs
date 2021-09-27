using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{

    public float paddleSpeed = 1;

    private Vector3 playerPos = new Vector3(0, -9.5f, 0);

    void Update()
    {
        ApplyPlayerInput();
    }

    void ApplyPlayerInput(){
        float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
        playerPos = new Vector3(Mathf.Clamp(xPos, -8f, 8f), -9.5f, 0);
        transform.position = playerPos;
    }
}
