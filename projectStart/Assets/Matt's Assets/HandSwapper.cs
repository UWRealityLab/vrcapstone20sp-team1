using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandSwapper : MonoBehaviour
{
    public SteamVR_Action_Boolean trigger;
    bool inRighthand = false;
    public Transform rightHand;
    public Transform leftHand;
    public Transform sword;
    public ThrowNinjaStarVR throwStar;
    bool listening;

    // Start is called before the first frame update
    void Start()
    {
        
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
            transform.SetParent(rightHand);
            sword.SetParent(rightHand);
        }
        else
        {
            transform.SetParent(leftHand);
            sword.SetParent(rightHand);
        }
        sword.localPosition = new Vector3(0, 0, 0);
        sword.localEulerAngles = new Vector3(0, 0, 0);
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
