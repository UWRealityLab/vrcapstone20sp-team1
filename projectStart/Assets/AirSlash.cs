﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlash : MonoBehaviour, Weapon
{

    public int damageMultiplier = 5;
    public float range = 25;
    float duration;
    float destroyDelay = .1f;
    
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
            StartCoroutine(DestroySelf());
        }
    }

    public int damage()
    {
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
        return Mathf.RoundToInt(velocity.magnitude) * damageMultiplier;
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destroyDelay);
        transform.Find("Audio Source").parent = null;
        Destroy(gameObject);
    }
}
