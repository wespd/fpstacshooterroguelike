using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement1 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;

    public float drag;

    public float notMovingDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float jumpGracePeriod;
    float coyoteTimeCounter;

    bool canJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    bool isGrounded;
    public float extraDistance;
    RaycastHit ray;
    bool hit;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public Transform orientation;

    [Header("UI")]
    public TMP_Text text;
    public int decimals;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics.Raycast(transform.position, Vector3.down,out ray, playerHeight *.5f + extraDistance);
        isGrounded = hit && ray.collider.GetComponent<canJumpOn>() != null;
        if(isGrounded)
        {
            coyoteTimeCounter = jumpGracePeriod;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        Inputs();
        LimitSpeed();
        DisplayPlayerInformation();
    }

    void FixedUpdate()
    {
        MovePlayer();
        ApplyDrag();
    }

    void Inputs()
    {
        verticalInput = 0;
        horizontalInput = 0;
        if(Input.GetKey(forward))
        {
            verticalInput++;
        }
        if(Input.GetKey(backward))
        {
           verticalInput--;
        }
        if(Input.GetKey(left))
        {
            horizontalInput--;
        }
        if(Input.GetKey(right))
        {
            horizontalInput++;
        }
    
        if(Input.GetKey(jumpKey) && canJump && coyoteTimeCounter > 0)
        {
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * acceleration * airMultiplier);
        }
    }

    void LimitSpeed()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0 , rb.velocity.z);

        if(velocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
    void Jump()
    {
        canJump = false;

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        canJump = true;
    }

    void ApplyDrag()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0 , rb.velocity.z);
        Vector3 dragVelocity = velocity.normalized * drag;
        bool isInputs = !(horizontalInput == 0 && verticalInput == 0);
        if(!isInputs)
        {
            rb.velocity = new Vector3(rb.velocity.x * notMovingDrag, rb.velocity.y, rb.velocity.z * notMovingDrag);
        }
        if(dragVelocity.x > rb.velocity.x)
        {
            dragVelocity = new Vector3(rb.velocity.x, 0, dragVelocity.y);
        }
        else if(rb.velocity.x < moveSpeed *.1f && !isInputs)
        {
            dragVelocity = new Vector3(0, 0, dragVelocity.y);
        }
        if(dragVelocity.z > rb.velocity.z)
        {
            dragVelocity = new Vector3(dragVelocity.x, 0, rb.velocity.z);
        }
        else if(rb.velocity.z < moveSpeed *.1f && !isInputs)
        {
            dragVelocity = new Vector3(dragVelocity.x, 0, 0);
        }
        
        rb.AddForce(-dragVelocity);
        
    }
    void DisplayPlayerInformation()
    {
        text.text = ($"speed:{rb.velocity.magnitude.ToString($"F{decimals}")},combined:{new Vector2(rb.velocity.x, rb.velocity.y).magnitude.ToString($"F{decimals}")}, x:{rb.velocity.x.ToString($"F{decimals}")}, z:{rb.velocity.z.ToString($"F{decimals}")}");
    }
}
