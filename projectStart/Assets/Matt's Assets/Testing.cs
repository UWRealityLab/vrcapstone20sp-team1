using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Testing : MonoBehaviour
{
    


    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnDisable()
    {
        transform.SetParent(null);
    }

}
