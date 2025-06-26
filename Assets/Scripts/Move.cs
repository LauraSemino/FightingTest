using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Move : MonoBehaviour
{

    public Rigidbody rb;
    public Vector3 localVel;
    public Vector3 moveInput;
    public float walkSpeed;

    public Vector3[] inputHistory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            moveInput.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveInput.x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveInput.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveInput.y = -1;
        }

    }

    private void FixedUpdate()
    {
        localVel = rb.velocity;

        localVel.x = moveInput.x * walkSpeed;


        rb.velocity = localVel;
    }
}
