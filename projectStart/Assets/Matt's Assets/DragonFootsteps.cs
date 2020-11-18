using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DragonFootsteps : MonoBehaviour
{
    public AudioSource rightFoot;
    public AudioSource leftFoot;
    public AudioClip footstep1;
    public AudioClip footstep2;

 

    void Thud1()
    {
        rightFoot.PlayOneShot(footstep1);
    }
    void Thud2()
    {
        leftFoot.PlayOneShot(footstep2);
    }
}
