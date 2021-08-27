using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Valve.VR;

public class FireBreath : MonoBehaviour
{
    ParticleSystem fire;
    /*
    AudioSource fireAudioStart;
    AudioSource fireAudioSustain;
    AudioSource fireAudioEnd;
    AudioSource[] audioList;
    */
    public AudioClip fireAudioStart;
    public AudioClip fireAudioSustain;
    public AudioClip fireAudioEnd;
    public AudioClip fireWindup;
    AudioSource fireAudioSource;
    public Animator animator;

    public SteamVR_Action_Single trigger;
    float triggerValue;
    float previousTriggerValue;
    float triggerThreshold = .8f;
    bool fireOn = false;
    bool previousFireState = false;
    public float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        //FireStop();
        fire = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        /*
        audioList = GetComponents<AudioSource>();
        foreach (AudioSource a in audioList)
        {
            if (a.clip.name == "Flame Thrower Start")
            {
                fireAudioStart = a;
            }
            if (a.clip.name == "Flame Thrower Sustain")
            {
                fireAudioSustain = a;
            }
            if (a.clip.name == "Flame Thrower End")
            {
                fireAudioEnd = a;
            }
        }
        */
        fireAudioSource = gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            fireOn = true;
        }
        else
        {
            fireOn = false;
        }
        
        if(fireOn == true && previousFireState == false)
        {
            Debug.Log("Start Coroutine");
            StartCoroutine(StartDelay());
        }
        else if(fireOn == false && previousFireState == true)
        {
            FireStop();
        }
        */
        /*
        if (fireAudioSource.isPlaying == false && fireAudioSource.clip.name == fireAudioStart.name)       
        {
            fireAudioSource.clip = fireAudioSustain;
            fireAudioSource.loop = true;
            fireAudioSource.Play();            
        }
        if (fireOn == false)
        {
            fire.Stop();
        }

        //previousTriggerValue = triggerValue;
        previousFireState = fireOn;
        */
    }
    public void FireDelay()
    {
        StartCoroutine(StartDelay());
    }


    void FireStart()
    {
        //Debug.Log("Fire Start");
        // Particle System
        fire.Play();
        // Audio
        fireAudioSource.clip = fireAudioStart;
        fireAudioSource.loop = false;
        fireAudioSource.Play();
        StartCoroutine(SwitchToSustainAudio());
    }

    public void FireStop()
    {
        fire.Stop();
        StopAllCoroutines();
        fireAudioSource.clip = fireAudioEnd;
        fireAudioSource.loop = false;
        fireAudioSource.PlayOneShot(fireAudioEnd);
    }
    public void AbruptStop()
    {
        StopAllCoroutines();
        //fireAudioSource.clip = null;
        fire.Stop();
        fireOn = false;
        previousFireState = true;
        fireAudioSource.Stop();
        
    }

    IEnumerator StartDelay()
    {
        //Debug.Log("Start Delay");
        fireAudioSource.clip = fireWindup;
        fireAudioSource.loop = false;
        fireAudioSource.Play();
        yield return new WaitForSeconds(delayTime);
        fireAudioSource.Stop();
        //Debug.Log("About to Start Fire");
        FireStart();
    }
    IEnumerator SwitchToSustainAudio()
    {
        yield return new WaitForSeconds(fireAudioStart.length);
        fireAudioSource.clip = fireAudioSustain;
        fireAudioSource.loop = true;
        fireAudioSource.Play();
    }
}
