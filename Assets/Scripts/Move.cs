using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using NodeCanvas.Tasks.Actions;
using System.Linq;
using ParadoxNotion.Serialization.FullSerializer;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using ParadoxNotion;

public class Move : MonoBehaviour
{
    public GameObject model;

    public Rigidbody rb;
    public Vector3 localVel;
    public Vector2 moveInput;
    public float walkSpeed;
    float moveSpeed;
    public string currentState;
    Coroutine currentCoroutine;

    public float facingDir; //1 is right, -1 is left

    public float runSpeed;
    public float runAccel;

    public float buttonHeldTimer;

    public float backDashTime;
    public float backDashSpeed;

    public float airSpeed;
    public float jumpHeight;
    bool doJump;
    public bool isGrounded;

    Vector3 lastInput;
    public Vector3 currentInput;
    public List<Vector3> inputHistory;

    public GameObject enemyPlayer;

    
   
    // Start is called before the first frame update
    void Start()
    {
        currentState = "None";
        facingDir = 1;
        moveSpeed = walkSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(moveSpeed);
        
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
            currentInput = (new Vector3(moveInput.x * facingDir, moveInput.y, buttonHeldTimer));
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveInput.x = 1;        
            currentInput = (new Vector3(moveInput.x * facingDir, moveInput.y, buttonHeldTimer));
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveInput.y = 1;     
            currentInput = (new Vector3(moveInput.x * facingDir, moveInput.y, buttonHeldTimer));
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveInput.y = -1;  
            currentInput = (new Vector3(moveInput.x * facingDir, moveInput.y, buttonHeldTimer));
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

        //checking for run
        if (currentInput.x > 0)
        {
            if (inputHistory[29].x == 0 && inputHistory[29].z <= 0.18)
            {
                if (inputHistory[28].x > 0 && inputHistory[28].z < 0.18)
                {                    
                    Run();
                }
            }
        }
        if (currentState == "Run" && (currentInput.x <= 0 || currentInput.y < 0))
        {
            
            currentState = "None";
        }
        if (currentState != "Run")
        {
            moveSpeed = walkSpeed;
        }

        //checking for backdash
        if (currentInput.x < 0 && currentInput.z < 0.2)
        {
            if (inputHistory[29].x == 0 && inputHistory[29].z <= 0.18)
            {
                if (inputHistory[28].x < 0 && inputHistory[28].z < 0.18 && (currentState == "None" || currentState == "Run"))
                {
                    currentCoroutine = StartCoroutine(Backdash());
                }
            }
        }

       //check for crouch
       if (currentInput.y < 0 && (currentState == "None" || currentState == "Run" ))
        {
            currentState = "Crouch";
            model.transform.localScale = new Vector3(1.5f, 0.75f, 1.5f);
            model.transform.position = new Vector3(model.transform.position.x, 1.25f,model.transform.position.z);
        }

       //check for uncrouch
        if(currentInput.y >= 0 && currentState == "Crouch") 
        {            
            model.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            model.transform.position = new Vector3(model.transform.position.x, 2f, model.transform.position.z);
            currentState = "None";
        }
        //check for jump
        if (currentInput.y > 0 && (currentState == "None" || currentState == "Run"))
        {
            doJump = true;
        }
    }
   

    private void FixedUpdate()
    {

        localVel = rb.velocity;
        if (currentState == "None" || currentState == "Run")
        {
            localVel.x = moveInput.x * moveSpeed;
        }

        //jump
        if (doJump && isGrounded == true)
        {
            localVel.y = jumpHeight;
            doJump = false;
        }
        else if (doJump == true && isGrounded == false)
        {
            doJump = false;
        }



        //facing direction
        if(transform.position.x > enemyPlayer.transform.position.x && currentState != "Run")
        {
            facingDir = -1;
        }
        else if (transform.position.x < enemyPlayer.transform.position.x && currentState != "Run")
        {
            facingDir = 1;
        }

        rb.velocity = localVel;
    }

    void Run()
    {
        Debug.Log("running");      
        currentState = "Run";            
       
        if (moveSpeed < runSpeed)
        {
            moveSpeed += runAccel * Time.deltaTime;
        }
        else
        {
            moveSpeed = runSpeed;
        }

    }
    
    IEnumerator Backdash()
    {
        currentState = "Backdash";
        
        yield return new WaitForFixedUpdate();
        rb.velocity = backDashSpeed*Vector3.left*facingDir;
        yield return new WaitForSecondsRealtime(backDashTime);
        rb.velocity = Vector3.zero;
        yield return new WaitForFixedUpdate();
        currentState = "None";
    }


    void OnCollisionEnter(Collision ground)
    {
        if (ground.gameObject.layer == 3)
        {          
            currentState = "None";
        }
    }
    void OnCollisionStay(Collision ground)
    {
        if (ground.gameObject.layer == 3)
        {
            isGrounded = true;
            
        }
    }
    void OnCollisionExit(Collision ground)
    {
        if (ground.gameObject.layer == 3)
        {
            isGrounded = false;
            currentState = "Jump";
        }
    }
}
