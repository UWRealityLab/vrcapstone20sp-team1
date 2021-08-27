using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

public class Testing : MonoBehaviour
{
    
    

    // Start is called before the first frame update
    void Start()
    {
 
    }

    private void Update()
    {
       
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Monster Detects Collision with " + other.gameObject.name);
    }




}
