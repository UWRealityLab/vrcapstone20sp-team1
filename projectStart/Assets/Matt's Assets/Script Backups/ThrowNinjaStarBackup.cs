﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ThrowNinjaStarBackup : MonoBehaviour
{
    public GameObject starObject;
    public GameObject starSpawn;
    public Hand hand;
    public string[] enemyTags;
    public AudioClip throwStarSound;

    [Tooltip("Scalar value defining how aggressive the assist is, higher means more assist")]
    public float aimAssistValue;
    [Tooltip("The angle from center to edge of the cone that aim assist is applied")]
    public float aimAssistConeAngle;

    private Vector3 prevHandPos;
    private GameObject star;
    private bool isActive;
    void Start()
    {
        isActive = true;
    }

    void Update()
    {
        if (hand.GetGrabStarting() == GrabTypes.Pinch && isActive)
        {
            hand.TriggerHapticPulse(2000);
            SpawnStar();
        }
        else if (hand.GetGrabEnding() == GrabTypes.Pinch && isActive)
        {
            Throw();
            hand.TriggerHapticPulse(2000);
        }
        prevHandPos = hand.transform.position;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Unactivate()
    {
        isActive = false;
    }

    private void Throw()
    {
        NinjaStar ns = star.GetComponent<NinjaStar>();
        //GameObject target = this.gameObject.GetComponent<HighlightEnemy>().getHighlightedEnemy();

        List<GameObject> enemies = CollectEnemies();
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


        if (smallestAngle < aimAssistConeAngle)
        {
            Debug.Log("aim assist applied, velocity: " + handVelocity.magnitude);
            ns.setAimAssist(target, handVelocity.magnitude * aimAssistValue);
        }
        GetComponent<AudioSource>().PlayOneShot(throwStarSound);
        star = null;
    }

    private void SpawnStar()
    {
        star = Instantiate(starObject, starSpawn.transform.position, starSpawn.transform.rotation) as GameObject;
        hand.AttachObject(star, GrabTypes.Pinch);
    }

    private List<GameObject> CollectEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach (string tag in enemyTags)
        {
            enemies.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }
        return enemies;
    }
}