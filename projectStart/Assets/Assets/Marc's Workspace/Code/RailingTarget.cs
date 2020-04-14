using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RailingTarget : MonoBehaviour
{
    public GameObject destructable;
    public Rigidbody anchor;

    public Func<bool> destroyConditionFunc { get; set; } = null;
    public Func<Vector3> velocityFunc {get; set;} = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(velocityFunc != null)
            anchor.velocity = velocityFunc();

        if (destroyConditionFunc != null && destroyConditionFunc())
        {
            Destroy(this.gameObject);
        }
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
