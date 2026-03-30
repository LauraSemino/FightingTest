using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharBase : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 localVel;
    public Vector3 moveInput;
    public float walkSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        localVel = rb.linearVelocity;

        


        rb.linearVelocity = localVel;
    }
}
