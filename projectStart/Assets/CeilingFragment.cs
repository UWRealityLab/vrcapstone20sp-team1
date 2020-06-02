using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFragment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag.Equals("dragon"));
        if (other.gameObject.tag.Equals("dragon"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.transform.root.gameObject.tag);
        if (other.collider.transform.root.gameObject.tag.Equals("dragon"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
