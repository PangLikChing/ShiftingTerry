using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidGuyMovementStartScene : MonoBehaviour
{
    float randomJumpTimeInterval, randomMoveTimeInterval, canvasLeftEdge, canvasRightEdge, moveForceX;
    Vector2 forceUp = new Vector2(0, 125);
    bool isJumping;
    float timeIntervalJump, timeIntervalMove;

    void Start()
    {
        isJumping = false;
        randomJumpTimeInterval = Random.Range(1.5f, 3f);
        randomMoveTimeInterval = 0.5f;

        canvasLeftEdge = -2.5f;
        canvasRightEdge = 2.5f;
    }

    void FixedUpdate()
    {
        //Jumping
        if (timeIntervalJump < randomJumpTimeInterval)
        {
            timeIntervalJump += Time.deltaTime;
        }
        else
        {
            Jump();
        }

        //Moving
        if (timeIntervalMove < randomMoveTimeInterval)
        {
            timeIntervalMove += Time.deltaTime;
        }
        else
        {
            if (!isJumping)
            {
                moveLeftOrRight();
            }
        }
    }

    void Jump()
    {
        isJumping = true;
        transform.GetComponent<Rigidbody2D>().AddForce(forceUp);

        timeIntervalJump = 0;
        randomJumpTimeInterval = Random.Range(1.5f, 3f);
    }

    void moveLeftOrRight()
    {
        //left = 0, right = 1
        int leftRight = Random.Range(0, 2);
        moveForceX = Random.Range(75f, 125f);

        stopMovingXAxis();

        if (leftRight == 0)
        {
            if (transform.position.x <= canvasLeftEdge / 3 * 2)
            {
                //go right
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(moveForceX, 0));
            }
            else
            {
                //go left
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveForceX, 0));
            }
        }
        else if (leftRight == 1)
        {
            if (transform.position.x >= canvasRightEdge / 3 * 2)
            {
                //go left
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveForceX, 0));
            }
            else
            {
                //go right
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(moveForceX, 0));
            }
        }

        timeIntervalMove = 0;
        randomMoveTimeInterval = Random.Range(0.5f, 1f);
    }

    void stopMovingXAxis()
    {
        float yVelocity = transform.GetComponent<Rigidbody2D>().velocity.y;
        Vector2 stopForce = new Vector2(0, yVelocity);
        transform.GetComponent<Rigidbody2D>().velocity = stopForce;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            isJumping = false;
        }
    }
}
