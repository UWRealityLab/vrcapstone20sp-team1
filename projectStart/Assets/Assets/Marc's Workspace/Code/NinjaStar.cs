using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar : MonoBehaviour, Weapon
{

    public float damageMultiplier;
    public float durationBeforeVanishSeconds;
    public GameObject poof;

    private GameObject targetEnemy;
    private float aimAssistVelocity;
    private Vector3 velocity = Vector3.zero;

    private float throwTime = float.MaxValue;

    void Update()
    {
        if (targetEnemy != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, 
                                                    targetEnemy.transform.position, 
                                                    ref velocity, 
                                                    0.05f,
                                                    aimAssistVelocity);
        }

        /* Code to disappear on timeout
        if(Time.time > throwTime + durationBeforeVanishSeconds)
        {
            Instantiate(poof, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        */
    }

    void OnCollisionEnter(Collision collision)
    {
        /* code to disappear on collision */
        Instantiate(poof, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
    public int damage()
    {
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
        return Mathf.RoundToInt(Mathf.Sqrt((velocity.x * velocity.x) + (velocity.z * velocity.z)) * damageMultiplier);
    }

    public void setAimAssist(GameObject target, float aimAssistVelocity)
    {
        this.targetEnemy = target;
        this.aimAssistVelocity = aimAssistVelocity;
    }

    public void setThrowTime(float time)
    {
        this.throwTime = time;
    }
}