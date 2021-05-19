using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar : MonoBehaviour, Weapon
{

    public int damageValue;
    // public float damageMultiplier;
    public float durationBeforeVanishSeconds;
    public GameObject poof;
    public AudioClip hitTargetSound;
    public AudioClip hitEnemySound;
    public AudioClip hitWoodSound;
    public AudioClip hitWaterSound;

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
        //destroyStar(collision.collider.material.name);
        destroyStar(collision.gameObject);
    }


    //public void destroyStar(string matName = "default")
    public void destroyStar(GameObject hitObject)
    {
        bool destroyWithPoof = true;
        if (hitObject.name == "Standing Log Target(Clone)")
        {
            GetComponent<AudioSource>().PlayOneShot(hitWoodSound);
        }
        else if (hitObject.tag == "monster" || hitObject.tag == "monster1" || hitObject.tag == "monster2" || hitObject.tag == "monster3" || hitObject.tag == "dragon" || hitObject.tag == "enemy")
        {
            GetComponent<AudioSource>().PlayOneShot(hitEnemySound);
        }
        else if (hitObject.tag == "ninjaStarTarget")
        {
            GetComponent<AudioSource>().PlayOneShot(hitTargetSound);
        }
        else if (hitObject.tag == "Water")
        {
            destroyWithPoof = false;
            GetComponent<AudioSource>().PlayOneShot(hitWaterSound);
        }
        else 
        {
            GetComponent<AudioSource>().PlayOneShot(hitWoodSound);
        }
            /*
            if (matName.Equals("Wood (Instance)"))
            {
                GetComponent<AudioSource>().PlayOneShot(hitTargetSound);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(hitEnemySound);
            }
            */
            /* code to disappear on collision */
            if (destroyWithPoof == true) {
                Instantiate(poof, transform.position, transform.rotation);
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
                this.GetComponent<Rigidbody>().detectCollisions = false;
            }

        Destroy(this.gameObject,0.1f);
    }
    public int damage()
    {
        //Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
        //return Mathf.RoundToInt(Mathf.Sqrt((velocity.x * velocity.x) + (velocity.z * velocity.z)) * damageMultiplier);
        return damageValue;
    }

    public void setAimAssist(GameObject target, float aimAssistVelocity)
    {
        this.targetEnemy = target;
        this.aimAssistVelocity = aimAssistVelocity;
    }

}