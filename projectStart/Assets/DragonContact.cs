﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonContact : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("dragon"))
        {
            rb.constraints = RigidbodyConstraints.None;
        }   
    }
}
