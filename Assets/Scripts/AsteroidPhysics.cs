using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPhysics : MonoBehaviour
{
    public float speed;
    //(x,y)
    Vector2 direction;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        direction = Vector2.one.normalized;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check Collision 
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Wall":
                direction.y = -direction.y;
                break;
            case "WallLeftRight":
                direction.x = -direction.x;
                break;
            case "Star":
                direction.x = -direction.x;
                direction.y = -direction.y;
                break;
            case "Asteroid":
                direction.x = -direction.x;
                direction.y = -direction.y;
                break;
            case "Player":
                direction.x = -direction.x;
                direction.y = -direction.y;
                break;
        }

    }
}
