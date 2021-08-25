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
        triggerValue = trigger.GetAxis(SteamVR_Input_Sources.Any);
        if (triggerValue >= triggerThreshold && previousTriggerValue < triggerThreshold)
        {
            FireStart();
        }
        if (triggerValue <= triggerThreshold && previousTriggerValue > triggerThreshold)
        {
            FireStop();
        }
        */
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
            StartCoroutine(StartDelay());
        }
        else if(fireOn == false && previousFireState == true)
        {
            FireStop();
        }
        
        if (fireAudioSource.isPlaying == false && fireAudioSource.clip.name == fireAudioStart.name)       
        {
            fireAudioSource.clip = fireAudioSustain;
            fireAudioSource.loop = true;
            fireAudioSource.Play();
        }

        //previousTriggerValue = triggerValue;
        previousFireState = fireOn;
    }

    void FireStart()
    {
        // Particle System
        fire.Play();
        // Audio
        fireAudioSource.clip = fireAudioStart;
        fireAudioSource.loop = false;
        fireAudioSource.Play();
    }

    void FireStop()
    {
        fire.Stop();
        fireAudioSource.clip = fireAudioEnd;
        fireAudioSource.loop = false;
    }
    IEnumerator StartDelay()
    {
        fireAudioSource.clip = fireWindup;
        fireAudioSource.loop = false;
        fireAudioSource.Play();
        yield return new WaitForSeconds(delayTime);
        fireAudioSource.Stop();
        FireStart();
    }
}
