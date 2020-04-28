using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ThrowNinjaStarVR : MonoBehaviour
{
    public GameObject starObject;
    public GameObject starSpawn;
    public Hand hand;

    [Tooltip("Scalar value defining how aggressive the assist is, higher means more assist")]
    public float aimAssistValue;
    [Tooltip("The angle from center to edge of the cone that aim assist is applied")]
    public float aimAssistConeAngle;

    private Vector3 prevHandPos;

    private GameObject star;
    GameManager manager;
    void Start()
    {
        manager = GameManager.GetInstance();

    }
    // Update is called once per frame
    void Update()
    {
        if(hand.GetGrabStarting() == GrabTypes.Pinch && !manager.GetLevel().Equals("final") && !manager.GetLevel().Equals("end"))
        {
            hand.TriggerHapticPulse(2000); // doesn't work yet
            SpawnStar();
        } 
        else if (hand.GetGrabEnding() == GrabTypes.Pinch && !manager.GetLevel().Equals("final") && !manager.GetLevel().Equals("end")) 
        {
            Throw();
            hand.TriggerHapticPulse(2000); // doesn't work yet
        }
        prevHandPos = hand.transform.position;
    }

    private void Throw()
    {
        NinjaStar ns = star.GetComponent<NinjaStar>();
        //GameObject target = this.gameObject.GetComponent<HighlightEnemy>().getHighlightedEnemy();

        GameObject[] enemies;

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject target = null;
        float smallestAngle = float.MaxValue;

        Vector3 handVelocity = (hand.transform.position - prevHandPos) / Time.deltaTime;

        foreach (GameObject enemy in enemies)
        {

            Vector3 handToTarget = enemy.transform.position - hand.transform.position;
            float angle = Vector3.Angle(handVelocity, handToTarget);

            if (smallestAngle.CompareTo(angle) > 0)
                {
                    smallestAngle = angle;
                    target = enemy;
                }
        }
        

        if (smallestAngle< aimAssistConeAngle)
        {
            Debug.Log("aim assist applied, velocity: " + handVelocity.magnitude);
            ns.setAimAssist(target, handVelocity.magnitude * aimAssistValue);
        }
        ns.setThrowTime(Time.time);
        star = null;
    }

    private void SpawnStar()
    {
        star = Instantiate(starObject, starSpawn.transform.position, starSpawn.transform.rotation) as GameObject;
        hand.AttachObject(star, GrabTypes.Pinch);
    }
}
