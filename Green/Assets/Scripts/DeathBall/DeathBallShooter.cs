using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBallShooter : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private PlayerMovement playerMovement;

    public float speedMultiplier = 18f;
    // Start is called before the first frame update
    void Start()
    {
        ball.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        ball.SetActive(true);
        ball.transform.position = gameObject.transform.position;

        //Get direction
        float horizontal = playerMovement.horizontal;
        float vertical = playerMovement.vertical;
        Vector2 direction = new Vector2(horizontal, vertical);
        direction.Normalize();
        //Get Rigidbody
        Rigidbody2D playerRb = gameObject.GetComponent<Rigidbody2D>();
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = playerRb.velocity / 4;


        rb.AddForce(direction * speedMultiplier, ForceMode2D.Impulse);
    }
}