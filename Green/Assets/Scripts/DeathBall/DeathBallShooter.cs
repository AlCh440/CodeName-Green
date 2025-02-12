using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBallShooter : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject aim;

    public float speedMultiplier = 18f;
    // Start is called before the first frame update
    void Start()
    {
        ball.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cable"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        ball.SetActive(true);
        ball.transform.position = gameObject.transform.position;

        //Get direction
        Vector2 vector = new Vector2(-gameObject.transform.position.x + aim.transform.position.x, -gameObject.transform.position.y + aim.transform.position.y);
        float divident = Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y));
        Vector2 direction = vector / divident; //direction to 1
        
        //Get Rigidbody
        Rigidbody2D playerRb = gameObject.GetComponent<Rigidbody2D>();
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = playerRb.velocity / 4;
        rb.AddForce(direction * speedMultiplier, ForceMode2D.Impulse);


    }
}