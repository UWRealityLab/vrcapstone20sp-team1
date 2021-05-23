using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandSwapperV2 : MonoBehaviour
{
    public SteamVR_Action_Boolean trigger;
    bool inRighthand = false;
    public Hand rightHand;
    public Hand leftHand;
    public GameObject sword;
    public Transform swordPoof;
    public ThrowNinjaStarVR throwStar;
    bool listening;
    
    Vector3 swordAttachRotation;
    Vector3 starAttachRotation;
    Vector3 swordAttachPosition;
    Vector3 starAttachPosition;
    bool starTutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Delay());
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
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.1f);
        //leftHand.AttachObject(sword, GrabTypes.Pinch, Hand.AttachmentFlags.ParentToHand);
        leftHand.AttachObject(sword, GrabTypes.Pinch);
    }
    void SwapHands()
    {
        inRighthand = !inRighthand;
        if (inRighthand)
        {
            leftHand.DetachObject(sword);
            rightHand.AttachObject(sword, GrabTypes.Pinch, Hand.AttachmentFlags.ParentToHand);
            transform.SetParent(leftHand.transform);
            sword.GetComponent<Sword>().SetHand(rightHand);
            if(starTutorial == true)
            {
                ControllerButtonHints.ShowButtonHint(leftHand, leftHand.uiInteractAction);
                ControllerButtonHints.HideButtonHint(rightHand, rightHand.uiInteractAction);
            }
        }
        else
        {
            rightHand.DetachObject(sword);
            leftHand.AttachObject(sword, GrabTypes.Pinch, Hand.AttachmentFlags.ParentToHand);
            transform.SetParent(rightHand.transform);
            sword.GetComponent<Sword>().SetHand(leftHand);
            if (starTutorial == true)
            {
                ControllerButtonHints.HideButtonHint(leftHand, leftHand.uiInteractAction);
                ControllerButtonHints.ShowButtonHint(rightHand, rightHand.uiInteractAction);
            }
        }
        swordPoof.SetParent(sword.transform.parent);
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
    public void StartStarTutorial()
    {
        starTutorial = true;
        if (inRighthand == true)
        {
            ControllerButtonHints.ShowButtonHint(leftHand, leftHand.uiInteractAction);
        }
        else
        {
            ControllerButtonHints.ShowButtonHint(rightHand, rightHand.uiInteractAction);
        }
    }
    public void EndStarTutorial()
    {
        starTutorial = false;
        ControllerButtonHints.HideButtonHint(leftHand, leftHand.uiInteractAction);
        ControllerButtonHints.HideButtonHint(rightHand, rightHand.uiInteractAction);
    }
    /*
    private void OnDisable()
    {
        if (rightHand.ObjectIsAttached(sword))
        {
            rightHand.DetachObject(sword);
        }
        if (leftHand.ObjectIsAttached(sword))
        {
            leftHand.DetachObject(sword);
        }
    }
    */
}
