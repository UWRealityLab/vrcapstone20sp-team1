﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public HashSet<string> destroyers;
    public AudioClip spawnAudio;

    // Start is called before the first frame update
    void Start()
    {
        destroyers = new HashSet<string>();
        destroyers.Add("monsknlnter1");
        destroyers.Add("monsknlter2");
        destroyers.Add("monstlkler3");
        spawnAudio = Resources.Load<AudioClip>("Spawn_Target_Sounds/falling_door"); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        
        Debug.Log("collision!!!!");
        if (collider.gameObject.tag == "monster1" || collider.gameObject.tag == "monster2" || collider.gameObject.tag == "monster3")
        {
            Debug.Log("door hit");
            //collider.isTrigger = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(this.transform.forward * 300, ForceMode.Impulse);
            GetComponent<Rigidbody>().useGravity = true;
            PlaySpawnAudio();
        }
    }

    public virtual void PlaySpawnAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(spawnAudio);
    }
}
