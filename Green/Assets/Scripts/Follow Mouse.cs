using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector2 mousePos;
    private float disctanceToPlayer = 1.5f;

    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private CableShooter shooter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = GetMousePosition();
        Vector2 vec = GetVectorDivided(mousePos);
        vec *= (disctanceToPlayer + shooter.timeHolding);



        gameObject.transform.position = playerRb.position + vec;
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

    
}
