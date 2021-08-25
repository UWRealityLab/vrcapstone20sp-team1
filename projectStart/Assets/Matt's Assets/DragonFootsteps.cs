using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DragonFootsteps : MonoBehaviour
{
    public AudioSource rightFoot;
    public AudioSource leftFoot;
    public AudioSource dragon;

    public AudioClip footstep1;
    public AudioClip footstep2;
    public AudioClip swipeGrunt;
    public AudioClip lungeGrunt;
    public AudioClip flinchAudio;

    public FireBreath fire;

 

    void Thud1()
    {
        rightFoot.PlayOneShot(footstep1);
    }
    void Thud2()
    {
        leftFoot.PlayOneShot(footstep2);
    }
    void Swipe()
    {
        dragon.PlayOneShot(swipeGrunt);
    }
    void Lunge()
    {
        dragon.PlayOneShot(lungeGrunt);
    }
    void StopAudio()
    {
        //dragon.Stop();
        fire.AbruptStop();
        dragon.PlayOneShot(flinchAudio);
    }
}
