using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ThrowNinjaStarVR : MonoBehaviour
{
    public GameObject starObject;
    public GameObject starSpawn;
    public Hand hand;

    private GameObject star;

    // Update is called once per frame
    void Update()
    {
        if(hand.GetGrabStarting() == GrabTypes.Pinch)
        {
            hand.TriggerHapticPulse(2000); // doesn't work yet
            SpawnStar();
        } 
        else if (hand.GetGrabEnding() == GrabTypes.Pinch) 
        {
            Throw();
            hand.TriggerHapticPulse(2000); // doesn't work yet
        }
    }

    private void Throw()
    {
        star = null;
    }

    private void SpawnStar()
    {
        star = Instantiate(starObject, starSpawn.transform.position, starSpawn.transform.rotation) as GameObject;

        hand.AttachObject(star, GrabTypes.Pinch);
    }
}
