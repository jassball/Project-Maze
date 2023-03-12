using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;
    
    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYscale;
    private float startYscale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState{
        walking,
        sprinting,
        crouching,
        air
    }

    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYscale = transform.localScale.y;
    }

    // Update is called once per frame
    private void Update() {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // Handle drag
        if(grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // When to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded && playerHealth.currentHealth>0) {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetKeyDown(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f,ForceMode.Impulse);
        }

        if(Input.GetKeyUp(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
        
    }

    private void StateHandler(){
        

        if(grounded && Input.GetKey(sprintKey)){
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        
        else if(grounded){
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else if(Input.GetKey(crouchKey)){
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        else{
            state = MovementState.air;
        }
    }

    private void MovePlayer() {

        if(playerHealth.currentHealth <= 0){
           return;
        }
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        // On Ground
        if(grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } 
        // In Air
        else if(!grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limit velocity
        if(flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump() {
        // Reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        readyToJump = true;
    }
}   

