using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidGuyCollision : MonoBehaviour
{
    public bool isFalling, isGameOver;
    StupidGuyMovement stupidGuyMovement;

    void Awake()
    {
        stupidGuyMovement = transform.GetComponent<StupidGuyMovement>();
    }

    void Start()
    {
        isFalling = false;
        isGameOver = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isFalling = false;

        if (collision.collider.tag == "Game Over Zone")
        {
            isGameOver = true;
        }
        else if (collision.collider.tag == "Floor")
        {
            isGameOver = true;
        }
        else if(collision.collider.tag == "Final Box")
        {
            StartCoroutine(resetJump());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isFalling = true;
    }

    IEnumerator resetJump()
    {
        yield return new WaitForSeconds(1);

        stupidGuyMovement.isJumping = false;
    }
}
