using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon :  Monster
{
    // Start is called before the first frame update
    public Animation anim;

    private bool isDying = false;
    void Start()
    {
        anim.Play("birth");
    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.isPlaying)
        {
            if(isDying)
            {
                Destroy(this.gameObject);
            }
            anim.Play("attack2");
        } 
    }

    public override void Death()
    {
        anim.Play("die");
        isDying = true;
    }
    public override void HitReaction()
    {
    }

    public virtual void PlayHitAudio()
    {
    }
}
