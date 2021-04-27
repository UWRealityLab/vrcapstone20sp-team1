using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

public class Testing : MonoBehaviour
{
    public SkeletonUIOptions skelOptions;


    // Start is called before the first frame update
    void Start()
    {
        skelOptions.ShowController();
    }


    private void OnDisable()
    {
    }

}
