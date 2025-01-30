using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableShooter : MonoBehaviour
{
    private GameObject hook;
    private float speed = 24f;
    public float timeHolding;
    public float timeHolded;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonUp("Cable") && hook == null)
        {
            timeHolded = timeHolding * 24f;
            ShootHook();
        }

        if (Input.GetButtonDown("Unattach") && hook != null)
        {
            Debug.Log("attampting delete");
            DeleteHook();
        }

        if (Input.GetKey("mouse 0"))
        {
            if (timeHolding < 2f)
            {
                timeHolding += 0.008f;
            }
            else if (timeHolding > 2f)
            {
                timeHolding = 2f;
            }
        }
        else
        {
            timeHolding = 0f;
        }
    }

    void ShootHook()
    {
        hook = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);

        Vector2 mouse = GetMousePosition();
        Vector2 divident = GetVectorDivided(mouse);

        Rigidbody2D rb = hook.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(timeHolded * divident.x, timeHolded * divident.y);
        //rb.position = new Vector2(divident.x, divident.y);
    }

    Vector2 GetMousePosition()
    {

        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z

        Vector2 pos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        return pos;
    }

    Vector2 GetVectorDivided(Vector2 pos)
    {
        Vector2 vector = new Vector2(-playerRb.position.x + pos.x, -playerRb.position.y + pos.y);
        float divident = Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y));
        Vector2 vec = new Vector2(vector.x / divident, vector.y / divident);
        return vec;
    }


    public float distanceFromPlayer()
    {
        return timeHolded * 3;
    }

    void DeleteHook()
    {
        bool t = true;
        DistanceJoint2D joint = hook.GetComponent<DistanceJoint2D>();

        if (joint.connectedBody != null)
        {
            GameObject obj = joint.connectedBody.gameObject;

            while (t)
            {
                if (obj.name != "Player")
                {
                    joint = obj.GetComponent<DistanceJoint2D>();
                    GameObject tempObj = joint.connectedBody.gameObject;

                    Destroy(obj);
                    obj = tempObj;
                }
                else
                {
                    t = false;
                }
            }
        }
        Destroy(hook);
    }
}
