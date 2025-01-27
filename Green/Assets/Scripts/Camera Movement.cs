using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    

    private void LateUpdate()
    {
        transform.position = target.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
