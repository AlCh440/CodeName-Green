using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMechanics : MonoBehaviour
{
    private bool attract = false;
    private float firstImpulse = 0.5f;
    private float attractionForce = 77f;
    private Rigidbody2D playerRb;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (attract)
        {
            //Add force
            Vector2 vector = gameObject.transform.position - player.transform.position;
            float divident = Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y));
            Vector2 finalVec = new Vector2(vector.x / divident * 0.8f, vector.y / divident * 0.8f); // Vector nearly independent of distance


            
            playerRb.AddForce(finalVec * attractionForce);
        }
    }

    public void AttractPlayer()
    {
        attract = true;
        //For now, add speed
        Vector2 vector = gameObject.transform.position - player.transform.position;
        float divident = Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y));
        Vector2 finalVec = new Vector2(vector.x / divident, vector.y / divident);

        playerRb.AddForce(finalVec*attractionForce * firstImpulse, ForceMode2D.Impulse);
    }

    public void StopAttract()
    {
        attract = false;
        Debug.Log("stoping");
    }
}
