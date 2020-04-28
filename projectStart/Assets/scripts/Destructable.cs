using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject shatteredversion;
    public HashSet<string> destroyers;
    bool destroyed;

    public int scalar; // Cracked instances have a different scale than the normal ones, put the ratio here.
    // Start is called before the first frame update
    void Start()
    {
        destroyers = new HashSet<string>();
        destroyers.Add("sphere");
        destroyers.Add("ninjaStar");
        destroyers.Add("sword");
        
        destroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (destroyers.Contains(collision.gameObject.tag) && !destroyed)
        {
            ShatterObject();
            destroyed = true;
        }
    }

    void ShatterObject()
    {
        if (shatteredversion != null)
        {
            GameObject instance = Instantiate(shatteredversion, transform.position, transform.rotation);
            instance.transform.localScale = transform.localScale * scalar; 
        }
        Destroy(gameObject);
    }
}
