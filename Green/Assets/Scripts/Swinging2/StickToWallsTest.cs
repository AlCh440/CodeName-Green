using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToWallsTest : MonoBehaviour
{
    private CableShooterTest follow;
    private float distance = 2.5f;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("PlayerTest");
        GameObject obj = GameObject.Find("Aim Reticle");
        follow = player.GetComponent<CableShooterTest>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector2 Normalize(Vector3 pos1, Vector3 pos2) // Set vector distance to 1
    {
        Vector2 vector = new Vector2(pos2.x - pos1.x, pos2.y - pos1.y); // we set vector to coordenates 0,0

        vector.Normalize();
        return vector;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            //Stick there
            Rigidbody2D comp = gameObject.GetComponent<Rigidbody2D>();
            comp.velocity = Vector2.zero;
            comp.bodyType = RigidbodyType2D.Static;

            float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);

            float iterations = dis / distance;
            Vector2 norm = Normalize(player.transform.position, gameObject.transform.position);
            norm *= distance;
            Vector2 spawnPos = player.transform.position;

            
            player.GetComponent<CableShooterTest>().CreateJoint(gameObject);
            
        }

    }
}
