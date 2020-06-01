using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RailingTarget : MonoBehaviour
{
    public GameObject destructable;
    public Rigidbody anchor;

    public Func<Vector3> velocityFunc {get; set;} = null;
    public SpawnRailingTarget targetSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(velocityFunc != null)
            anchor.velocity = velocityFunc();

        if(GetDestructable() == null)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        targetSystem.DecrementActiveTargets();
    }

    public GameObject GetDestructable()
    {
        return destructable;
    }

    public Rigidbody GetAnchor()
    {
        return anchor;
    }
}
