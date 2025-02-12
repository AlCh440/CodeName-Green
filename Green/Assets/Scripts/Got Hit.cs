using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotHit : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private BallMechanics ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Damage"))
       {
           ball.AttractPlayer();
       }

    }

}
