using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody2D playerRb;
    private float dashingVelocity = 20f;
    private float dashingTime = 0.18f;
    private Vector2 dashingDir;
    public bool isDashing = false;
    public bool canDash = true;
    private float originalGravity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool dashInput = Input.GetButtonDown("Dash");

        if (dashInput && canDash)
        {
            Debug.Log("dash");
            isDashing = true;
            canDash = false;

            float horizontal = playerMovement.horizontal;
            float vertical = playerMovement.vertical;

            dashingDir = new Vector2(horizontal, vertical);
            dashingDir.Normalize();
            StartCoroutine(StopDashing());

            //Stop gravity
            originalGravity = playerRb.gravityScale;
            playerRb.gravityScale = 0f;
        }



        if (isDashing)
        {

            playerRb.velocity = dashingDir * dashingVelocity;
        }


    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        playerRb.gravityScale = originalGravity;

        if (dashingDir.y != 0)
        {
            Vector2 vec = new Vector2(playerRb.velocity.x, playerRb.velocity.y - dashingDir.y*dashingVelocity/3f);
            playerRb.velocity = vec;
        }
        isDashing = false;
        
    }
}
