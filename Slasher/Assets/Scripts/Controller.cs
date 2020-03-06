using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private float jumpHeight = 5f;
    public static bool isGrounded = false;
    private float speed = 2f;

    private Animator anim;
    private Rigidbody2D rb;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!Player.isAttacking && !Player.isHit && !Player.isDead)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                if (isGrounded)
                {
                    anim.Play("run");
                }
                transform.localScale = new Vector3(1, 1, 1);

            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!Player.isAttacking && !Player.isHit && !Player.isDead)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                if (isGrounded) 
                { 
                    anim.Play("run"); 
                }
                transform.localScale = new Vector3(-1, 1, 1);
                transform.GetChild(2).localScale = new Vector3(-1,1,1);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            if (isGrounded && !Player.isAttacking && !Player.isHit && !Player.isDead)
            {
                anim.Play("player_idle");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !Player.isDead)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        if (rb.velocity.y > 0 && !Player.isAttacking)
        {
            anim.Play("jump_up");
        }
        if (rb.velocity.y < 0 && !Player.isAttacking)
        {
            anim.Play("jump_fall");
        }
    }
}
