using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Sword : MonoBehaviour, Weapon
{

    //public float damageMultiplier;
    public float fadeInDurationinS;
    public GameObject tip;
    public GameObject hilt;
    public GameObject slash;
    public Hand hand;
    public Camera cam;
    public AudioSource source;
    public AudioClip airSlashSound;
    public AudioClip hitWoodSound;
    public AudioClip hitEnemySound;

    public float slashSpeedThreshold = 8f;
    public float slashDurationThreshold = 10f; // how many calls to fixedupdate (called 90 times per second)
    private short count = 0;
    private Vector3 startTipPos;
    private Vector3 startHiltPos;
    private bool slashThresholdsMet;

    private Vector3 prevTipPos;

    private Color[] originalColors;
    private float initTime;

    private Vector3 prevHandPos;

    public SteamVR_Action_Vibration vibration;
    float hapticDuration = .1f;
    float frequency = 160;
    float intensity = .5f;
    // Start is called before the first frame update
    void Start()
    {
        Material[] materials = gameObject.GetComponent<MeshRenderer>().materials;
        this.originalColors = new Color[materials.Length];
        this.initTime = Time.time;
        for(int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
            materials[i].color = new Color(materials[i].color.r, materials[i].color.g, materials[i].color.b, 0.0f);
        }
        prevTipPos = tip.transform.position;

    }

    void FixedUpdate()
    {
        AirSlashLogic();
    }

    void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }

    void OnTriggerEnter(Collider other)
    {
        //hand.TriggerHapticPulse(ushort.MaxValue);
        vibration.Execute(0, hapticDuration, frequency, intensity, hand.handType);
        if (other.tag == "monster" || other.tag == "monster1" || other.tag == "monster2" || other.tag == "monster3" || other.tag == "dragon" || other.tag == "enemy")
        {
            source.PlayOneShot(hitEnemySound);
        }
        else if (other.tag == "Ground")
        {
            source.PlayOneShot(hitWoodSound);
        }
    }

    public int damage()
    {
        Vector3 velocity = (prevHandPos - transform.parent.position) / Time.deltaTime;
        //return Mathf.RoundToInt(Mathf.Sqrt((velocity.x * velocity.x) + (velocity.z * velocity.z)) * damageMultiplier);
        return Mathf.RoundToInt(15f + 15f * (velocity.magnitude - 350) / 30f);
    }

    public void SetHand(Hand hand)
    {
        this.hand = hand;
    }

    public void SetCamera(Camera cam)
    {
        this.cam = cam;
    }

    private void AirSlashLogic()
    {
        
        Vector3 bladeDirection = this.transform.right;
        Vector3 swordVelocity = CalculateTipVelocity();

        //Debug.Log(Vector3.Angle(swordVelocity, bladeDirection));
        float speed = swordVelocity.magnitude;
        if(speed >= slashSpeedThreshold && Vector3.Angle(swordVelocity, bladeDirection) < 90)
        {
            if(count == 0)
            {
                startTipPos = tip.transform.position;
                startHiltPos = hilt.transform.position;
            }
            count++;
            if(count > slashDurationThreshold)
            {
                slashThresholdsMet = true;
            }
        }
        else if (speed != 0)
        {
            if(slashThresholdsMet)
            {
                Vector3 tipArcCenter = ((0.5f * startTipPos) + (1.5f*tip.transform.position)) / 2;
                Vector3 hiltArcCenter = ((startHiltPos * 0.5f) + (hilt.transform.position) * 1.5f) / 2;
                Vector3 direction = Vector3.Normalize(tipArcCenter - hiltArcCenter + cam.transform.forward * 3f);

                hand.TriggerHapticPulse(65535);

                Vector3 slashAngle = (tip.transform.position - startTipPos);
                

                GameObject slsh = Instantiate(slash, tipArcCenter, Quaternion.FromToRotation(Vector3.right, slashAngle));

                slsh.GetComponent<Transform>().localScale = new Vector3((tip.transform.position - startTipPos).magnitude, 0.01f, 0.01f);
                slsh.GetComponent<Rigidbody>().AddForce(direction * 800);

               //source.PlayOneShot(airSlashSound);
                /*
                Debug.Log("SLASH! " + count);
                Debug.Log("tiparccenter: " + tipArcCenter +
                            "\nhiltarccenter: " + hiltArcCenter);
                Debug.Log("\ndirection: " + direction);
                Debug.Log("\nfromto: " + (tip.transform.position - startTipPos));
                */

                slashThresholdsMet = false;
            }
            count = 0;
        }

    }
    private Vector3 CalculateTipVelocity()
    {
        // Debug.Log(tip.transform.position);
        //Debug.Log(prevTipPos);
        Vector3 currentPosition = tip.transform.position;
        Vector3 velocity = (currentPosition - prevTipPos) / Time.deltaTime;
        //Debug.Log(velocity);
        prevTipPos = currentPosition;
        return velocity;
    }
}