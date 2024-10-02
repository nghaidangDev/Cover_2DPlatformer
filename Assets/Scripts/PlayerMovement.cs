using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int forceJump;

    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        //Flip
        if (horizontal > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontal < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetBool("walk", horizontal != 0);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, forceJump);
            anim.SetTrigger("jump");
            anim.SetBool("isground", isGrounded);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
