using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using NodeCanvas.Tasks.Actions;
using System.Linq;
using ParadoxNotion.Serialization.FullSerializer;

public class Move : MonoBehaviour
{

    public Rigidbody rb;
    public Vector3 localVel;
    public Vector2 moveInput;
    public float walkSpeed;
    public float buttonHeldTimer;

    public Vector3 lastInput;
    public Vector3 currentInput;
    public List<Vector3> inputHistory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Vector3.zero;
        lastInput = currentInput;

        buttonHeldTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A))
        {
            buttonHeldTimer = 0;
            inputHistory.Add(lastInput);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            buttonHeldTimer = 0;
            inputHistory.Add(lastInput);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            buttonHeldTimer = 0;
            inputHistory.Add(lastInput);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            buttonHeldTimer = 0;
            inputHistory.Add(lastInput);
        }


        if (Input.GetKey(KeyCode.A))
        {
            moveInput.x = -1;
            //buttonHeldTimer += Time.deltaTime;
            currentInput = (new Vector3(moveInput.x, moveInput.y, buttonHeldTimer));
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveInput.x = 1;
            //buttonHeldTimer += Time.deltaTime;
            currentInput = (new Vector3(moveInput.x, moveInput.y, buttonHeldTimer));
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveInput.y = 1;
            //buttonHeldTimer += Time.deltaTime;
            currentInput = (new Vector3(moveInput.x, moveInput.y, buttonHeldTimer));
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveInput.y = -1;
            //buttonHeldTimer += Time.deltaTime;
            currentInput = (new Vector3(moveInput.x, moveInput.y, buttonHeldTimer));
        }
        if (moveInput == Vector2.zero)
        {
            currentInput = (new Vector3(moveInput.x, moveInput.y, buttonHeldTimer));
           
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            inputHistory.Add(lastInput);          
            buttonHeldTimer = 0;
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            inputHistory.Add(lastInput);
            buttonHeldTimer = 0;
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            inputHistory.Add(lastInput);
            buttonHeldTimer = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            inputHistory.Add(lastInput);
            buttonHeldTimer = 0;
        }


        

        if (inputHistory.Count > 30)
        {
            
            inputHistory.RemoveAt(0);
        }


        
    }
   

    private void FixedUpdate()
    {
        localVel = rb.velocity;

        localVel.x = moveInput.x * walkSpeed;
                    
        rb.velocity = localVel;
    }
}
