using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class EquipSword : MonoBehaviour
{
    public GameObject swordObject;
    public GameObject swordSpawn;
    public Hand hand;

    private GameObject sword;

    // Update is called once per frame
    void Update()
    {
        if (hand.GetGrabStarting() != GrabTypes.None)
        {
            hand.TriggerHapticPulse(2000); // doesn't work yet
            Equip();
        }
        else if (hand.GetGrabEnding() != GrabTypes.None)
        {
            Uneqiup();
            hand.TriggerHapticPulse(2000); // doesn't work yet
        }
    }

    private void Uneqiup()
    {
        Destroy(sword);
        sword = null;
    }

    private void Equip()
    {
        sword = Instantiate(swordObject, swordSpawn.GetComponent<Transform>()) as GameObject;

        hand.AttachObject(sword, GrabTypes.Pinch);
    }
}

