using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class EquipSwordBackup : MonoBehaviour
{
    public GameObject swordObject;
    public GameObject swordSpawn;
    public GameObject emitObject;

    public Hand hand;
    public Camera cam;

    private GameObject sword;

    // Update is called once per frame
    private void Start()
    {
        Equip();
    }
    void Update()
    {
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

