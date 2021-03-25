using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandSwapper : MonoBehaviour
{
    public SteamVR_Action_Boolean trigger;
    bool inRighthand = false;
    public Transform rightHandAttach;
    public Transform leftHandAttach;
    public Transform sword;
    public Transform swordPoof;
    public ThrowNinjaStarVR throwStar;
    bool listening;
    Vector3 swordAttachRotation;
    Vector3 starAttachRotation;
    Vector3 swordAttachPosition;
    Vector3 starAttachPosition;

    // Start is called before the first frame update
    void Start()
    {
        swordAttachRotation = leftHandAttach.localEulerAngles;
        swordAttachPosition = leftHandAttach.localPosition;
        starAttachRotation = rightHandAttach.localEulerAngles;
        starAttachPosition = rightHandAttach.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (listening)
        {
            if (inRighthand == false)
            {
                if (trigger.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    SwapHands();
                }
            }
            else
            {
                if (trigger.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    SwapHands();
                }
            }
        }
    }
    IEnumerator Listening()
    {
        while(listening == true)
        {
            if (inRighthand == false) 
            {
                if (trigger.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    SwapHands();
                }
            }
            else
            {
                if (trigger.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    SwapHands();
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
    void SwapHands()
    {
        inRighthand = !inRighthand;
        if (inRighthand)
        {
            transform.SetParent(leftHandAttach);
            leftHandAttach.localEulerAngles = starAttachRotation;
            leftHandAttach.localPosition = starAttachPosition;
            sword.SetParent(rightHandAttach);
            rightHandAttach.localEulerAngles = swordAttachRotation;
            rightHandAttach.localPosition = swordAttachPosition;
            swordPoof.SetParent(rightHandAttach);
        }
        else
        {
            transform.SetParent(rightHandAttach);
            rightHandAttach.localEulerAngles = starAttachRotation;
            rightHandAttach.localPosition = starAttachPosition;
            sword.SetParent(leftHandAttach);
            leftHandAttach.localEulerAngles = swordAttachRotation;
            leftHandAttach.localPosition = swordAttachPosition;
            swordPoof.SetParent(leftHandAttach);
        }
        sword.localPosition = new Vector3(0, 0, 0);
        sword.localEulerAngles = new Vector3(0, 0, 0);
        swordPoof.localPosition = new Vector3(0, 0, 0);
        swordPoof.localEulerAngles = new Vector3(0, 180, 0);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        throwStar.SwapActive();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sword")
        {
            listening = true;
            //StartCoroutine(Listening());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Sword")
        {
            listening = false;
        }
    }
}
