using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class EquipSword : MonoBehaviour
{
    public GameObject swordObject;
    public GameObject swordSpawn;
    public GameObject emitObject;

    public Hand hand;
    public Camera cam;

    private GameObject sword;

    // Update is called once per frame
    void Update()
    {
        if (hand.GetGrabStarting() == GrabTypes.Pinch)
        {
            hand.TriggerHapticPulse(2000);
            Equip();
        }
        else if (hand.GetGrabEnding() == GrabTypes.Pinch)
        {
            Uneqiup();
            hand.TriggerHapticPulse(2000);
        }
    }

    private void Uneqiup()
    {
        Destroy(sword);
        emitObject.GetComponent<ParticleSystem>().Play();
        sword = null;
    }

    private void Equip()
    {
        sword = Instantiate(swordObject, swordSpawn.GetComponent<Transform>()) as GameObject;

        hand.AttachObject(sword, GrabTypes.Pinch);
        sword.GetComponent<Sword>().SetHand(hand);
        sword.GetComponent<Sword>().SetCamera(cam);
    }
}

