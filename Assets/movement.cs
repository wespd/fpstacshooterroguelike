 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    public Rigidbody rB;
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode sprint;
    float lastFrameSpeed;
    public float jumpStrength;
    public float jumpDetectionHeight;
    [Range(0,1)]
    public float frictionAmount;
    [Range(1,10)]
    public float notMovingFrictionMultiplier;
    public float sprintMultiplier;
    public float maxSprint;
    public float currentSprint;
    public float sprintRechargeTime;
    float currentRechargeTime;
    bool isRecharging = true;
    public bool canMove = true;
    public Vector3 movementVector;
    public float wallJumpRange;
    public float wallJumpVerticalPower;
    public float wallJumpHorizontalPower;
    bool canWallJump;
    public float wallJumpMovementLockDuration;
    // Update is called once per frame
    void Update()
    {
        movementVector = Vector3.zero;

        float acceleration = speed/Mathf.Max(rB.velocity.magnitude, 1); 
        //rB.velocity = new Vector3(0, rB.velocity.y, 0);
        if(canMove)
        {
            if(isGrounded())
            {
                if(Input.GetKeyDown(jump))
                {
                    rB.AddForce(transform.up * jumpStrength, ForceMode.Impulse);
                }
            }
        
            if(Input.GetKey(forward))
            {
                movementVector += transform.forward;
            }
            if(Input.GetKey(backward))
            {
                movementVector -= transform.forward;
            }
            if(Input.GetKey(left))
            {
                movementVector -= transform.right;
            }
            if(Input.GetKey(right))
            {
                movementVector += transform.right;
            }
            if(movementVector != Vector3.zero)
            {
                rB.AddForce(movementVector.normalized * acceleration * Time.deltaTime, ForceMode.Acceleration);
                rB.AddForce(new Vector3(-rB.velocity.x, 0, -rB.velocity.z) * frictionAmount, ForceMode.Acceleration);
            }
            else
            {
                rB.AddForce(movementVector.normalized * acceleration * Time.deltaTime, ForceMode.Acceleration);
                rB.AddForce(new Vector3(-rB.velocity.x, 0, -rB.velocity.z) * frictionAmount * notMovingFrictionMultiplier, ForceMode.Acceleration);
            }
            
            
            if(Input.GetKey(jump) && !isGrounded())
            {
                RaycastHit wallJumpHit;
                Physics.Raycast(transform.position, movementVector, out wallJumpHit, wallJumpRange); 
                WallJump(wallJumpHit);
            }
            
           
        }
        if(movementVector == Vector3.zero && rB.angularVelocity.magnitude < 1)
        {
            rB.angularVelocity = Vector3.zero;
        }
        if(movementVector == Vector3.zero && rB.velocity.magnitude < 1)
        {
            rB.velocity = Vector3.zero;
        }
    
    
        speed = lastFrameSpeed;
    }
    
    public bool isGrounded()
    {
        RaycastHit hit;
        bool hitObject = Physics.SphereCast(transform.position, transform.localScale.x/2, -transform.up, out hit, transform.localScale.y + jumpDetectionHeight);
        if(hit.collider != null && hit.collider.GetComponent<canJumpOn>() != null)
        {
            canWallJump = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void WallJump(RaycastHit hit)
    {        
        if(hit.collider != null && canWallJump)
        {
            Debug.Log("One Wall Bounce");
            rB.velocity = (hit.normal * wallJumpHorizontalPower) + (transform.up * wallJumpVerticalPower);
            canWallJump = false;
            canMove = false;
            StartCoroutine(MovementCooldown(wallJumpMovementLockDuration));
        }
    }

    IEnumerator MovementCooldown(float duration)
    {
        yeild return new waitForSeconds(duration);
        canMove = true;
    }
}
