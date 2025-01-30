using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToWalls : MonoBehaviour
{
    private CableShooter follow;
    private float distance = 2.5f;
    public GameObject nodePrefab;
    private GameObject lastNode;
    private GameObject actualNode;
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

            lastNode = null;
            actualNode = null;
            for (int i = 0; i < iterations; i++)
            {
                spawnPos += norm;

                // Create node
                if (!(i > iterations - 1))
                {
                    actualNode = Instantiate(nodePrefab, spawnPos, Quaternion.identity);
                }



                // if there is a node before attach them, else attach to player, if its the last, attach to hook
                if (lastNode == null)
                {
    
                    // first procedure
                    DistanceJoint2D joint = actualNode.GetComponent<DistanceJoint2D>();
                    joint.enabled = true;
                    joint.distance = distance;
                    joint.connectedBody = player.GetComponent<Rigidbody2D>();

                }
                else if (lastNode != null)
                {
                    if (!(i > iterations - 1))
                    {
               
                        // last procedure 
                        DistanceJoint2D joint = actualNode.GetComponent<DistanceJoint2D>();
                        joint.enabled = true;
                        joint.distance = distance;
                        joint.connectedBody = lastNode.GetComponent<Rigidbody2D>();
                    }
                    else
                    {
 
                        // standart procedure
                        DistanceJoint2D joint = gameObject.GetComponent<DistanceJoint2D>();
                        joint.enabled = true;
                        joint.distance = distance;
                        joint.connectedBody = lastNode.GetComponent<Rigidbody2D>();
                    }
                }

                //assign lastnode
                lastNode = actualNode;
            }
        }

    }
}
