using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableShooter : MonoBehaviour
{
    private GameObject hook;
    //private float speed = 24f;
    public float timeHolding;
    public float timeHolded;
    [SerializeField] private GameObject prefabHook;
    [SerializeField] private GameObject prefabRect;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Rigidbody2D playerRb;
    

    private float distance = 2.5f;
    public GameObject nodePrefab;
    private GameObject lastNode;
    private GameObject actualNode;

    public int typeOfSwing;

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
        hook = Instantiate(prefabHook, gameObject.transform.position, Quaternion.identity);

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
        if (typeOfSwing == 1)
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
        else if (typeOfSwing == 2)
        {
            bool t = true;
            SpringJoint2D joint = hook.GetComponent<SpringJoint2D>();

            if (joint.connectedBody != null)
            {
                GameObject obj = joint.connectedBody.gameObject;

                while (t)
                {
                    if (obj.name != "Player")
                    {
                        joint = obj.GetComponent<SpringJoint2D>();
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
        else if (typeOfSwing == 3)
        {
            Destroy(hook);
        }
        else if (typeOfSwing == 4)
        {
            Destroy(hook);
        }
        else if (typeOfSwing == 5)
        {
            bool t = true;
            HingeJoint2D joint = hook.GetComponent<HingeJoint2D>();

            if (joint.connectedBody != null)
            {
                GameObject obj = joint.connectedBody.gameObject;

                while (t)
                {
                    if (obj.name != "Player")
                    {
                        joint = obj.GetComponent<HingeJoint2D>();
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

    public void ChangeType(int i)
    {
        Debug.Log("Changing type: " + i);
        typeOfSwing = i;
    }

    Vector2 Normalize(Vector3 pos1, Vector3 pos2) // Set vector distance to 1
    {
        Vector2 vector = new Vector2(pos2.x - pos1.x, pos2.y - pos1.y); // we set vector to coordenates 0,0

        vector.Normalize();
        return vector;
    }

    public void CreateCable(GameObject obj) // next idea is hinge joint between rectangles 
    {
        if (typeOfSwing == 1 || typeOfSwing == 2) ComplexCable(obj);
        else if (typeOfSwing == 3 || typeOfSwing == 4) SimpleCable(obj);
        else if (typeOfSwing == 5) ComplexCableRect(obj);
    }

    public void SimpleCable(GameObject obj)
    {
        Debug.Log("simplecalbe");
        float dis = Vector2.Distance(gameObject.transform.position, obj.transform.position);

        if (typeOfSwing == 3) CreateDistanceJoint(obj, gameObject, dis);
        else if (typeOfSwing == 4) CreateSpringJoint(obj, gameObject, dis);
    }

    public void ComplexCable(GameObject obj)
    {
        float dis = Vector2.Distance(gameObject.transform.position, obj.transform.position);

        float iterations = dis / distance;
        Vector2 norm = Normalize(gameObject.transform.position, obj.transform.position);
        norm *= distance;
        Vector2 spawnPos = gameObject.transform.position;

        lastNode = null;
        actualNode = null;
        for (int i = 0; i < iterations; i++)
        {
            spawnPos += norm;

            // Create node
            if (!(i > iterations - 1)) actualNode = Instantiate(nodePrefab, spawnPos, Quaternion.identity);

            // if there is a node before attach them, else attach to gameObject, if its the last, attach to hook
            if (lastNode == null)
            {
                if (typeOfSwing == 1) CreateDistanceJoint(actualNode, gameObject, distance);
                else if (typeOfSwing == 2) CreateSpringJoint(actualNode, gameObject, distance);
            }
            else if (lastNode != null)
            {
                if (!(i > iterations - 1))
                {
                    // last procedure 
                    if (typeOfSwing == 1) CreateDistanceJoint(actualNode, lastNode, distance);
                    else if (typeOfSwing == 2) CreateSpringJoint(actualNode, lastNode, distance);
                }
                else
                {
                    // standart procedure
                    if (typeOfSwing == 1) CreateDistanceJoint(obj, lastNode, distance);
                    else if (typeOfSwing == 2) CreateSpringJoint(obj, lastNode, distance);
                }
            }

            //assign lastnode
            lastNode = actualNode;
        }
    }

    private void ComplexCableRect(GameObject obj)
    {
        float dis = Vector2.Distance(gameObject.transform.position, obj.transform.position);

        float iterations = dis / 0.4f;
        Vector2 norm = Normalize(gameObject.transform.position, obj.transform.position);
        norm *= 0.4f;
        Vector2 spawnPos = gameObject.transform.position;

        lastNode = null;
        actualNode = null;

        for (int i = 0; i < iterations; i++)
        {
            spawnPos += norm;

            // Create node
            if (!(i > iterations - 1)) actualNode = Instantiate(prefabRect, spawnPos, Quaternion.identity);

            // if there is a node before attach them, else attach to gameObject, if its the last, attach to hook
            if (lastNode == null)
            {
                Debug.Log("type 1");
                CreateHingeJoint(actualNode, gameObject, -0.5f, 0.0f);
            }
            else if (lastNode != null)
            {
                if (!(i > iterations - 1))
                {
                    // standart procedure
                    Debug.Log("type 2");
                    CreateHingeJoint(actualNode, lastNode, -0.5f, 0.5f);
                }
                else
                {
                    // l;ast procedure
                    Debug.Log("type 3");
                    CreateHingeJoint(obj, lastNode, 0.0f, 0.5f);
                }
            }

            //assign lastnode
            lastNode = actualNode;
        }
    }

    private void CreateDistanceJoint(GameObject firstNode, GameObject SecondNode, float distance)
    {
        // first procedure
        DistanceJoint2D joint = firstNode.GetComponent<DistanceJoint2D>();
        joint.enabled = true;
        joint.distance = distance;
        joint.connectedBody = SecondNode.GetComponent<Rigidbody2D>();
    }

    private void CreateSpringJoint(GameObject firstNode, GameObject SecondNode, float distance)
    {
        SpringJoint2D joint = firstNode.AddComponent<SpringJoint2D>();
        joint.connectedBody = SecondNode.GetComponent<Rigidbody2D>();
        joint.autoConfigureDistance = false;
        joint.distance = distance;
        //joint.spring = 4.5f;
        joint.dampingRatio = 1f;
        joint.frequency = 6.5f;
    }

    private void CreateHingeJoint(GameObject firstNode, GameObject secondNode, float firstOffSet, float secondOffSet)
    {
        HingeJoint2D joint = firstNode.AddComponent<HingeJoint2D>();
        joint.connectedBody = secondNode.GetComponent<Rigidbody2D>();
        joint.connectedBody = secondNode.GetComponent<Rigidbody2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = new Vector2 (0.0f, firstOffSet);
        joint.connectedAnchor = new Vector2 (0.0f, secondOffSet);
    }
}

