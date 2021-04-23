using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRabble : MonoBehaviour
{
    public AudioClip[] clips;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(source.isPlaying == false)
        {
            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
}
