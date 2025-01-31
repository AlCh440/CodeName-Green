using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToWalls : MonoBehaviour
{
    private CableShooter follow;
    
    
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        GameObject obj = GameObject.Find("Aim Reticle");
        follow = player.GetComponent<CableShooter>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            // Sticking
            Rigidbody2D comp = gameObject.GetComponent<Rigidbody2D>();
            comp.velocity = Vector2.zero;
            comp.bodyType = RigidbodyType2D.Static;

            player.GetComponent<CableShooter>().CreateCable(gameObject);
        }

    }
}
