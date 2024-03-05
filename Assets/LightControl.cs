using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public GameObject ball;
    public float moveSpeed;

    public BallState ballState = BallState.unsummoned;

    public Transform orientation;

    Vector3 direction;

    float distance = 0;

    [Header("Keybinds")]
    public KeyCode forward = KeyCode.Q;
    public KeyCode backward = KeyCode.E;

    public enum BallState
    {
        unsummoned,
        movingForwards,
        movingBackwards,
        painting,
        summoned


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(ballState == BallState.unsummoned)
            {
                ball.SetActive(true);
                ball.transform.position = Camera.main.transform.position + orientation.forward.normalized * 2;
                direction = Camera.main.transform.forward;
                ballState = BallState.movingForwards;
            }
            else if(ballState == BallState.movingForwards || ballState == BallState.movingBackwards)
            {
                direction = Vector3.zero;
                ballState = BallState.summoned;
            }
        }
        if(Input.GetMouseButtonDown(2) && ballState == BallState.summoned)
        {
            distance = Vector3.Distance(Camera.main.transform.position, ball.transform.position);
            ballState = BallState.painting;
        }
        else if(Input.GetMouseButtonUp(2))
        {
            ballState = BallState.summoned;
        }
        if(ballState == BallState.movingForwards)
        {
            ball.transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        if(ballState == BallState.movingBackwards)
        {
            ball.transform.Translate(-direction * moveSpeed * Time.deltaTime);
        }
        if(Input.GetKeyDown(forward) && (ballState == BallState.movingBackwards || ballState == BallState.summoned))
        {
            ballState = BallState.movingForwards;
            direction = (ball.transform.position - Camera.main.transform.position).normalized;
        }
        else if(Input.GetKeyDown(backward) && (ballState == BallState.movingForwards || ballState == BallState.summoned))
        {
            ballState = BallState.movingBackwards;
            direction = (ball.transform.position - Camera.main.transform.position).normalized;
        }

        
    }
    void LateUpdate()
    {
        if(ballState == BallState.painting)
        {
            ball.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        }
    }
}
