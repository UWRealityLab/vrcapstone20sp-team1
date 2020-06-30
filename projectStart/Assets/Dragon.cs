using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    // Start is called before the first frame update
    public Animation anim;
    public AudioClip roar;
    public AudioClip death;

    private bool isDying = false;
    void Start()
    {
        anim.Play("birth");
        this.GetComponent<AudioSource>().PlayOneShot(roar);
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.isPlaying)
        {
            if (isDying)
            {
                Destroy(this.gameObject);
            }
            anim.Play("attack2");
            GetComponent<AudioSource>().PlayOneShot(roar);
        }
    }

    public override void Death()
    {
        anim.Play("die");
        GetComponent<AudioSource>().PlayOneShot(death);
        isDying = true;
    }
    public override void HitReaction()
    {
    }

    public override void PlayHitAudio()
    {
        //GetComponent<AudioSource>().PlayOneShot(roar);
    }

}
