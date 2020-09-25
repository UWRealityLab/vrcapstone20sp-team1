using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestructEnv"))
        {
            Rigidbody rb = collision.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;

            /*
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            Debug.Log("Collision");
            */
        }
    }
}
