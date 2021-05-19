using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlash : MonoBehaviour, Weapon
{

    //public int damageMultiplier = 5;
    public int damageValue;
    public float range = 25;
    float duration;
    float destroyDelay = .1f;
    bool collided = false;
    
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        duration = range / GetComponent<Rigidbody>().velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnTime >= duration)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("MARC DEBUG LINE: Triggered");
        //this.GetComponent<Collider>().enabled = false;
        if (other.name != "Sword")
        {
            if (collided == false)
            {
                collided = true;
                StartCoroutine(DestroySelf());
            }
        }
    }

    public int damage()
    {
        //Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
        //return Mathf.RoundToInt(velocity.magnitude) * damageMultiplier;
        return damageValue;
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destroyDelay);
        transform.Find("Audio Source").parent = null;
        Destroy(gameObject);
    }
}
