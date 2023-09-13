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
    // Update is called once per frame
    void Update()
    {
        lastFrameSpeed = speed;
        movementVector = Vector3.zero;
        if(currentRechargeTime >= sprintRechargeTime)
        {
            isRecharging = true;
        }
        if(isRecharging == false)
        {
            if(currentRechargeTime <= sprintRechargeTime)
            {
                currentRechargeTime += 2*Time.deltaTime;
            }
           
        }
        else
        {
            if(currentSprint < maxSprint)
            {
                currentSprint += Time.deltaTime;
            }
            else
            {
                currentSprint = maxSprint;
            }
            
        }
        if(Input.GetKey(sprint) && currentSprint > 0 && isGrounded())
        {
            isRecharging = false;
            speed *= sprintMultiplier;
            currentSprint -= Time.deltaTime;
            currentRechargeTime = 0;
        }

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
            
            rB.AddForce(movementVector * acceleration, ForceMode.VelocityChange);
            rB.AddForce(new Vector3(-rB.velocity.x, 0, -rB.velocity.z) * frictionAmount);
            if(Input.GetKeyDown(jump) && !isGrounded())
            {
                RaycastHit wallJumpHit = Physics.Raycast(transform.position, movementVector, out wallJumpHit, wallJumpRange);
                WallJump(wallJumpHit);
            }
            
           
        }
        if(movementVector == Vector3.zero && isGrounded())
        {
            frictionAmount *= notMovingFrictionMultiplier;
        }
        if(movementVector == Vector3.zero && isGrounded())
        {
            frictionAmount /= notMovingFrictionMultiplier;
        }
    
        speed = lastFrameSpeed;
    }
    
    public bool isGrounded()
    {
        RaycastHit hit;
        bool hitObject = Physics.SphereCast(transform.position, transform.localScale.x/2, -transform.up, out hit, transform.localScale.y + jumpDetectionHeight);
        if(hit.collider != null && hit.collider.GetComponent<canJumpOn>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void WallJump(RaycastHit hit)
    {
        if(hit.collider != null)
        {
            rB.AddForce(hit.normal * wallJumpHorizontalPower + (Vector3.up * wallJumpVerticalPower));
        }
    }
}
