using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlash : MonoBehaviour, Weapon
{

    public int damageMultiplier = 5;
    public float duration = 2.5f;
    
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;    
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
        this.GetComponent<Collider>().enabled = false;
    }

    public int damage()
    {
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
        return Mathf.RoundToInt(velocity.magnitude) * damageMultiplier;
    }
}
