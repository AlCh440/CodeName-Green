using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyStop : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float frictionAmount = 0.18f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float vel = Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.y * rb.velocity.y));

        if (vel < frictionAmount) rb.velocity = Vector2.zero;
        else
        {
            Vector2 velo = rb.velocity;
            velo = (velo*frictionAmount) /  vel;

            rb.AddForce(-velo, ForceMode2D.Impulse);
        }
    }
}
