using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar : MonoBehaviour, Weapon
{

    public float damageMultiplier;
    public float durationBeforeVanishSeconds;
    public GameObject poof;
    public AudioClip hitTargetSound;
    public AudioClip hitEnemySound;

    private GameObject targetEnemy;
    private float aimAssistVelocity;
    private Vector3 velocity = Vector3.zero;

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
        destroyStar(collision.collider.material.name);
    }

    public void destroyStar(string matName = "default")
    {

        if (matName.Equals("Wood (Instance)"))
        {
            GetComponent<AudioSource>().PlayOneShot(hitTargetSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(hitEnemySound);
        }
        /* code to disappear on collision */
        Instantiate(poof, transform.position, transform.rotation);
        this.GetComponent<MeshRenderer>().enabled = false;
        Destroy(this.gameObject,0.2f);
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

}