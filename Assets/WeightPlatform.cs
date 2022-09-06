using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightPlatform : MonoBehaviour
{
    public Transform end;
    public Rigidbody2D rb;
    public BoxCollider2D collider;

    public Rigidbody2D Lou;

    public float speed;
    public bool moving;

    void Start()
    {
        rb.position = transform.position;
    }

    void FixedUpdate()
    {
        var move = 0;
        if (moving && rb.position.y > end.position.y) 
        {
            move = 1;
        }
        rb.MovePosition(new Vector2(rb.position.x, rb.position.y - 0.1f * (move * speed)));
        if (Lou != null) {
            Lou.velocity = Lou.velocity + rb.velocity;

        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        var contacts = other.contacts;
        bool topContact = false;
        foreach (var c in contacts)
        {
            if (c.normal.y < 0) {
                topContact = true;
            }
        }
        if (other.gameObject.name == "Lou" && topContact) 
        {
            Lou = other.transform.GetComponent<Rigidbody2D>();
            moving = true;

        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.name == "Lou")
        {
            moving = false;
        }
    }
}
