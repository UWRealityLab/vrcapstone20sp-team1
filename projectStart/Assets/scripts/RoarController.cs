using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarController : MonoBehaviour
{
    public AudioClip[] noise;
    public GameObject[] entries;

    void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "entry")
        {
            PlayHitAudio();
        }
    }
    public virtual void PlayHitAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(noise[Random.Range(0, noise.Length)]);
    }

}
