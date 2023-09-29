 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float maxSpeed;
    public Rigidbody rB;
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public float jumpStrength;
    public float jumpDetectionHeight;
    //[Range(10,30)]
    public float frictionAmount;
    public bool canMove = true;
    public Vector3 movementVector;
    public float wallJumpRange;
    public float wallJumpVerticalPower;
    public float wallJumpHorizontalPower;
    bool canWallJump;
    public float wallJumpMovementLockDuration;
    public float gravityAcceleration;
    public float gravitySnap;
    [Range(0.9f, 0.9999999999f)]
    public float getToMaxSpeedMultiplier;
    // Update is called once per frame
    void Update()
    {
        movementVector = Vector3.zero;
        bool isGrounded = this.isGrounded();
        
        if(isGrounded)
        {
            canWallJump = true;
        }
        if(canMove)
        {
            if(isGrounded)
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
            if(Input.GetKey(jump) && !isGrounded)
            {
                RaycastHit wallJumpHit;
                Physics.Raycast(transform.position, movementVector, out wallJumpHit, wallJumpRange); 
                WallJump(wallJumpHit);
            }
        }
        //rB.AddForce(movementVector.normalized * (maxSpeed - rB.velocity.magnitude) * getToMaxSpeedMultiplier * Time.deltaTime, ForceMode.VelocityChange);
        rB.AddForce(movementVector.normalized * maxSpeed * getToMaxSpeedMultiplier * Time.deltaTime, ForceMode.VelocityChange);
        if(rB.velocity.y < 0)
        {
            rB.AddForce(new Vector3(0, -gravityAcceleration, 0) * Time.deltaTime * gravitySnap, ForceMode.VelocityChange);
        }
        else
        {
            rB.AddForce(new Vector3(0, -gravityAcceleration, 0) * Time.deltaTime, ForceMode.VelocityChange);
        }
        
        rB.AddForce(frictionAmount * Time.deltaTime * new Vector3(-rB.velocity.x, 0 , -rB.velocity.z));
        if(movementVector == Vector3.zero && rB.angularVelocity.magnitude < 1)
        {
            rB.angularVelocity = Vector3.zero;
        }
        if(movementVector == Vector3.zero && rB.velocity.magnitude < 1)
        {
            rB.velocity = new Vector3(0, rB.velocity.y, 0);
        }
    
    
    }
    
    public bool isGrounded()
    {
        RaycastHit hit;
        bool hitObject = Physics.SphereCast(transform.position, transform.localScale.x/2, -transform.up, out hit, transform.localScale.y + jumpDetectionHeight);
        if(hit.collider && hit.collider.GetComponent<canJumpOn>())
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
        if(hit.collider && canWallJump)
        {
            rB.velocity = (hit.normal * wallJumpHorizontalPower) + (transform.up * wallJumpVerticalPower);
            canWallJump = false;
            canMove = false;
            StartCoroutine(MovementCooldown(wallJumpMovementLockDuration));
        }
    }

    IEnumerator MovementCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
}
