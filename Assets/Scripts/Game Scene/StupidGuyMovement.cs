using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidGuyMovement : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    float randomJumpTimeInterval, randomMoveTimeInterval, leftEdgeX, rightEdgeX, spriteWidth;
    float moveForceX;
    int leftRight;
    SpriteRenderer spriteRenderer;
    StupidGuyCollision stupidGuyCollision;
    Vector2 forceUp = new Vector2(0, 125);
    public bool isJumping;
    public float timeIntervalJump, timeIntervalMove;
    [SerializeField] GameObject box7;

    void Awake()
    {
        isJumping = false;
        randomJumpTimeInterval = Random.Range(2f, 4f);
        randomMoveTimeInterval = 0.5f;
        spriteRenderer = box7.GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.sprite.bounds.size.x * box7.transform.lossyScale.x;
        stupidGuyCollision = GetComponent<StupidGuyCollision>();
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
            if (!isFalling() && !stupidGuyCollision.isFalling && gameManager.LastBoxStable())
            {
                if((transform.position.x > (leftEdgeX + rightEdgeX) / 2 && leftRight == 0) || (transform.position.x < (leftEdgeX + rightEdgeX / 2) && leftRight == 1))
                {
                    Jump();
                }
                else
                {
                    stopMovingXAxis();
                    Jump();
                }
            }
        }

        //Moving
        if (timeIntervalMove < randomMoveTimeInterval)
        {
            timeIntervalMove += Time.deltaTime;
        }
        else
        {
            if (!isJumping && !isFalling() && !stupidGuyCollision.isFalling)
            {
                moveLeftOrRight();
            }
        }
    }

    bool isFalling()
    {
        //If gameobject's y velocity is smaller than -0.03, return true
        return GetComponent<Rigidbody2D>().velocity.y < -0.03 || GetComponent<Rigidbody2D>().velocity.y >= 0.03;
    }

    void Jump()
    {
        isJumping = true;
        transform.GetComponent<Rigidbody2D>().AddForce(forceUp);
        timeIntervalJump = 0;
        randomJumpTimeInterval = Random.Range(2f, 4f);
        Debug.Log(transform.GetComponent<Rigidbody2D>().velocity.x);
    }

    void moveLeftOrRight()
    {
        leftEdgeX = box7.transform.position.x - (spriteWidth / 2);
        rightEdgeX = box7.transform.position.x + (spriteWidth / 2);

        //left = 0, right = 1
        leftRight = Random.Range(0, 2);
        moveForceX = Random.Range(50f, 60f);

        stopMovingXAxis();

        if (leftRight == 0)
        {
            //if it's not very left
            if (transform.position.x >= (leftEdgeX + rightEdgeX) / 20 * 13)
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
        else if(leftRight == 1)
        {
            //if its not very right
            if(transform.position.x <= (leftEdgeX + rightEdgeX) / 20 * 7)
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

        timeIntervalMove = 0;
        randomMoveTimeInterval = Random.Range(0.75f, 1f);
    }

    void stopMovingXAxis()
    {
        float yVelocity = transform.GetComponent<Rigidbody2D>().velocity.y;
        Vector2 stopForce = new Vector2(0, yVelocity);
        transform.GetComponent<Rigidbody2D>().velocity = stopForce;
    }
}