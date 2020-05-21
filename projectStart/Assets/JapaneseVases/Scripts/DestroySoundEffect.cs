using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySoundEffect : MonoBehaviour {
    public AudioClip[] sounds;
	
	void Start () {
        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().pitch = 1 + Random.value * 0.2f;
            GetComponent<AudioSource>().clip = sounds[Random.Range(0, sounds.Length)];
            GetComponent<AudioSource>().Play();
        }
    }
}
