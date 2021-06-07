using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Monster
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Death()
    {
        GetComponent<AudioSource>().PlayOneShot(death);
        animator.SetBool("Run", false);
        animator.SetBool("Dying", true);
    }
    public override void HitReaction()
    {
        animator.SetTrigger("Flinch");
    }

    public override void PlayHitAudio()
    {
        //GetComponent<AudioSource>().PlayOneShot(roar);
    }
}
