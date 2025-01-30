using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDie : MonoBehaviour
{
    private int timeToDie;

    [SerializeField] private int framesPerSecond;
    [SerializeField] private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        timeToDie = seconds * framesPerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDie--;

        if (timeToDie <= 0)
        {
            Destroy(gameObject);
        }
    }
}
