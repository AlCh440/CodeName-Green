using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToWalls : MonoBehaviour
{
    private CableShooter follow;
    GameObject player;
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
        Debug.Log("checking collition");
        if (other.CompareTag("Ground"))
        {
            Debug.Log("attached");
            //Stick there
            Rigidbody2D comp = gameObject.GetComponent<Rigidbody2D>();
            comp.velocity = Vector2.zero;
            comp.bodyType = RigidbodyType2D.Static;

            //Activate joint
            DistanceJoint2D joint = gameObject.GetComponent<DistanceJoint2D>();
            joint.enabled = true;
            joint.distance = follow.distanceFromPlayer();
            joint.connectedBody = player.GetComponent<Rigidbody2D>();
        }
        
    }
}
