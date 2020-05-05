using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveVelocity : MonoBehaviour
{
    // Start is called before the first frame update
    public float x, y, z;

    void Start()
    {
        this.gameObject.GetComponentInChildren<Rigidbody>().velocity = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
