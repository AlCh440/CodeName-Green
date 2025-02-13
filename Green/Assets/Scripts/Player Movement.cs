using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public bool jump = false;
    public bool dash = false;
    public bool shoot = false;
    private float speed = 9f;
    private float acceleration = 13 * 1.3f;
    private float decceleration = 16 * 1.3f;
    private float jumpingPower = 13f * 1.3f;
    private float frictionAmount = 0.22f;
    private float velPower = 0.96f;
    private bool isFacingRight = true;

    //jumping
    private bool isJumping = false;
    private bool jumpInputReleased = true;
    private int lastGroundedTime = 0;
    private int lastJumpTime = 0;
    private int coyoteTime = -15;
    private float jumpCutMultiplier = 0.4f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PlayerDash playerDash;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jump = Input.GetButtonDown("Jump");
        dash = Input.GetButtonDown("Dash");
        shoot = Input.GetButtonDown("Shoot");

        if (horizontal > 0)
        {
            horizontal = 1;
        }
        else if (horizontal < 0)
        {
            horizontal = -1;
        }
        else horizontal = 0;

        IsGrounded();

        if (lastGroundedTime < 0 && !playerDash.isDashing)
        {
            if (jump)
            {
                rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
                lastGroundedTime = 0;
                lastJumpTime = 0;
                isJumping = true;
                jumpInputReleased = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }


        }
        else
        {
            //Jump slow descent
            if (jump && rb.velocity.y > 0f)
            {
                rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            }
        }

        lastGroundedTime++;
        lastJumpTime++;
        Flip();
    }

    private void IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer))
        {
            Debug.Log("standing");
            lastGroundedTime = coyoteTime;

            if (!playerDash.isDashing)
            {
                playerDash.canDash = true;
            }
        }
        else
        {
            isJumping = false;
        }

    }
    private void FixedUpdate()
    {
        if (!playerDash.isDashing)
        {
            // Basic movement
            float targetSpeed = horizontal * speed;
            float speedDif = targetSpeed - rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

            rb.AddForce(movement * Vector2.right);

            // Friction
            if (!isJumping && horizontal == 0)
            {
                float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

                amount *= Mathf.Sign(rb.velocity.x);

                rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            }
        }
        //Jumping

    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
