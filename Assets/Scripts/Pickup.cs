using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided with trigger " + collision.name);
        Destroy(transform.gameObject);
    }
}
