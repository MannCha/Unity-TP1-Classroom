using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;
    public BoxCollider2D collider;
    public Vector2 standingSize;
    public Vector2 crouchingSize;
    public Vector2 crouchingOffset;
    public float groundRayDistance;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    [SerializeField] private Rigidbody2D _rb;

    public float moveSpeed = 5f;
    public float jumpAmount = 10f;
    public bool onMovingPlatform;

    private void FixedUpdate()
    {
        if (this != PlayerManager._currentDog)
        {
            _rb.velocity = new Vector2(0, 0);
            animator.SetBool("moving", false);
            CrouchDog();
            return;
        }
        
        spriteRenderer.flipX = Inputs.FacingRight() ? false : Inputs.FacingLeft() ? true : spriteRenderer.flipX;
        if (!Inputs.IsMoving()) 
        {
            animator.SetBool("moving", false);
        } else {
            animator.SetBool("moving", true);
        }

        float verticalSpeed = _rb.velocity.y;

        if ((OnGround() && _rb.velocity.y == 0 || onMovingPlatform))
        {
            if (Inputs.switching) {

                PlayerManager.SwitchDog();
                
            }
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);

            if (Inputs.IsCrouching())
            {
                CrouchDog();
            }
            else {
                StandDog();
            }

            if (Inputs.IsJumping())
            {
                animator.SetBool("crouching", false);

                verticalSpeed = jumpAmount;
            }
        } else
        {
            if (verticalSpeed <= 0 ) 
            {
                if (!onMovingPlatform) 
                {
                    print("not on moving platform!");
                    animator.SetBool("falling", true);
                } else {
                    animator.SetBool("falling", false);
                }
            } else
            {
                animator.SetBool("jumping", true);
            }
            if (!Inputs.IsJumping())
            {   
                if (verticalSpeed > 10)
                {
                    verticalSpeed = 8f;
                }
            }
        }
        _rb.velocity = new Vector2((moveSpeed * Inputs.GetHorizontalInput()), verticalSpeed);
    }

    private void StandDog()
    {
        animator.SetBool("crouching", false);
        collider.size = standingSize;
        collider.offset = Vector2.zero;
    }

    private void CrouchDog()
    {
        animator.SetBool("crouching", true);
        collider.size = crouchingSize;
        collider.offset = crouchingOffset;
    }

    private bool OnGround()
    {

        Vector2 left = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
        Vector2 middle = new Vector2(collider.bounds.center.x, collider.bounds.min.y);
        Vector2 right = new Vector2(collider.bounds.max.x, collider.bounds.min.y);

        Vector2 direction = Vector2.down;

        Debug.DrawLine(left, (direction * groundRayDistance) + left, Color.green);
        Debug.DrawLine(middle, (direction * groundRayDistance) + middle, Color.green);
        Debug.DrawLine(right, (direction * groundRayDistance) + right, Color.green);

        RaycastHit2D [] hits = new RaycastHit2D[3];
        hits[0] = Physics2D.Raycast(left, direction, groundRayDistance, groundLayer);
        hits[1] = Physics2D.Raycast(middle, direction, groundRayDistance, groundLayer);
        hits[2] = Physics2D.Raycast(right, direction, groundRayDistance, groundLayer);

        bool ground = false;
        onMovingPlatform = false;
        foreach (var h in hits)
        {
            if (h.collider != null) 
            {
                Debug.Log(h.collider.gameObject.name);
                
                Debug.Log(h.collider.tag);

                if (!onMovingPlatform && h.collider.tag == "Weight") 
                {
                    Debug.Log("Moving platform detected");
                    onMovingPlatform = true;
                } 
                ground = true;
            }
        }
        
        return ground;
    }
}
