using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Testing : MonoBehaviour
{
    public SteamVR_Skeleton_Poser poser;
    public Hand hand;
    public SteamVR_Behaviour_Skeleton skel;
    int i = 3;
    // Start is called before the first frame update
    void Start()
    {
        if (i != 4)
        {
            Debug.Log(hand);
            Debug.Log(hand.skeleton);
            Debug.Log(poser);
            hand.skeleton.BlendToPoser(poser);
        }
        //poser.skeletonMainPose.leftHand;
    }

    // Update is called once per frame
    void Update()
    {
        

    }


}
