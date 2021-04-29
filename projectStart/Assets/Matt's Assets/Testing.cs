using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

public class Testing : MonoBehaviour
{
    public string printStatement;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Debug.Log("Trigger: " + printStatement);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision: " + printStatement);
    }


}
