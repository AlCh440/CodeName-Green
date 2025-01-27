using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableShooter : MonoBehaviour
{
    private GameObject hook;
    private float speed = 24f;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        //debug Log
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cable") && hook == null)
        {
            ShootHook();
        }
        else if (Input.GetButtonDown("Unattach") && hook != null)
        {
            DeleteHook();
        }
    }

    void ShootHook()
    {
        hook = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);

        Vector2 mouse = GetMousePosition();
        Vector2 divident = GetVectorDivided(mouse);
        Debug.Log(divident.x + " " + divident.y);

        Rigidbody2D rb = hook.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * divident.x, speed * divident.y);
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
    void DeleteHook()
    {
        Destroy(hook);
    }
}
