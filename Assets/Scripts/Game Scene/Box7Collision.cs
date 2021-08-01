using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box7Collision : MonoBehaviour
{
    public bool box7OnFloor;

    void Start()
    {
        box7OnFloor = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            box7OnFloor = true;
        }
    }
}
