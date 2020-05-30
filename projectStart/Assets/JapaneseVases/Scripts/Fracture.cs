using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fracture : MonoBehaviour {
    public Transform parts;
    private Transform breaked = null;
    private Vector3 velocity;
    public HashSet<string> destroyers;
    bool destroyed;

    void Start()
    {
        destroyers = new HashSet<string>();
        destroyers.Add("sphere");
        destroyers.Add("ninjaStar");
        destroyers.Add("sword");
        destroyers.Add("airSlash");

        destroyed = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        velocity = transform.GetComponent<Rigidbody>().velocity;

        
        if (destroyers.Contains(collision.gameObject.tag) && !destroyed)
        {
            Debug.Log("collition");
            Execute();
          //  destroyed = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if ((destroyers.Contains(other.gameObject.tag) || other == null) && !destroyed)
        {
            Debug.Log("trigger");
            Execute();
           // destroyed = true;
        }
    }


    public void Execute()
    { 
        if (breaked) return;
        Debug.Log("execute");

        breaked = (Transform)Instantiate(parts, transform.position, transform.rotation);
        breaked.localScale = transform.localScale;

        foreach (Transform part in breaked)
        {
            part.gameObject.GetComponent<Renderer>().materials[0].CopyPropertiesFromMaterial(gameObject.GetComponent<Renderer>().material);
            if (!part.gameObject.GetComponent<Rigidbody>())
            {
                part.gameObject.AddComponent<Rigidbody>();
                part.gameObject.GetComponent<Rigidbody>().velocity = velocity;
            }
            if (!part.gameObject.GetComponent<MeshCollider>())
            {
                part.gameObject.AddComponent<MeshCollider>();
                part.gameObject.GetComponent<MeshCollider>().convex = true;
            }

            float time = Random.Range(5f, 30f);
            Destroy(part.gameObject, time);
        }

        Destroy(breaked.gameObject, 30f);
        Destroy(gameObject);
    }
}
