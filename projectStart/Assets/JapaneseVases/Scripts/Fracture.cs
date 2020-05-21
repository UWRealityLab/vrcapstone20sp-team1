using UnityEngine;
using System.Collections;

public class Fracture : MonoBehaviour {
    public Transform parts;
    private Transform breaked = null;
    private Vector3 velocity;


    private void OnCollisionEnter(Collision collision)
    {
        velocity = transform.GetComponent<Rigidbody>().velocity;

        if (velocity.magnitude < 2.1f) return;
        Execute();
    }


    public void Execute()
    { 
        if (breaked) return;

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
