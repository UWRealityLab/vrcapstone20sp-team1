using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ThrowNinjaStarVR : MonoBehaviour
{
    public GameObject starObject;
    public GameObject starSpawn;
    public Hand hand;

    [Tooltip("The smaller the value the more aggressive the assist")]
    public float aimAssistValue;
    [Tooltip("The angle from center to edge of the cone that aim assist is applied")]
    public float aimAssistConeAngle;

    private Vector3 prevHandPos;

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
        prevHandPos = hand.transform.position;
    }

    private void Throw()
    {
        GameObject target = this.gameObject.GetComponent<HighlightEnemy>().getHighlightedEnemy();

        if (target != null)
        {
            Vector3 handVelocity = (hand.transform.position - prevHandPos) / Time.deltaTime;
            Vector3 handToTarget = target.transform.position - hand.transform.position;
            float angle = Vector3.Angle(handVelocity, handToTarget);
            Debug.Log("angle: " + angle);

            if (angle < aimAssistConeAngle)
            {
                Debug.Log("aim assist applied");
                NinjaStar ns = star.GetComponent<NinjaStar>();
                ns.setAimAssist(target, aimAssistValue);
            }
        }

        star = null;
    }

    private void SpawnStar()
    {
        star = Instantiate(starObject, starSpawn.transform.position, starSpawn.transform.rotation) as GameObject;

        hand.AttachObject(star, GrabTypes.Pinch);
    }
}
