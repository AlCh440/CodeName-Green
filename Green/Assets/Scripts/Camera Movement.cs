using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 initialPosition;
    private Vector3 target;
    private Vector3 startPos;
    private Vector3 interVec;
    private bool interpolation;
    private float interPerCent;
    private int interIncrease = 30;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        interpolation = false;
        transform.position = initialPosition;
    }



    private void LateUpdate()
    {
        if (interpolation)
        {

            interPerCent++;

            transform.position = Vector3.Lerp(startPos, target, interPerCent / 1000);
            Debug.Log(interPerCent);


            if (interPerCent == 1000)
            {
                interpolation = false;
            }
        }
    }


    public void ChangeTarget(Vector3 tar)
    {

        target = tar;
        startPos = transform.position;
        interVec = tar - startPos;

        interpolation = true;
        interPerCent = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
