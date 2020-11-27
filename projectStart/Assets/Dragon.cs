using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    // Start is called before the first frame update
    ////////////public Animation anim;
    public AudioClip roar;
    public AudioClip death;
    public AudioClip yelp;
    float destroyDelay = 3;

    private bool isDying = false;
    void Start()
    {
        //////////anim.Play("birth");
        this.GetComponent<AudioSource>().PlayOneShot(roar);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!anim.isPlaying)
        {
            if (isDying)
            {
                Destroy(this.gameObject);
            }
            anim.Play("attack2");
            GetComponent<AudioSource>().PlayOneShot(roar);
        }
        */
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            StartCoroutine(DestroyDelay(destroyDelay));
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Attack");
        }
    }

    public override void Death()
    {
        //////////anim.Play("die");
        GetComponent<AudioSource>().PlayOneShot(death);
        ///////isDying = true;
        animator.SetBool("isDying", true);
    }
    public override void HitReaction()
    {
        ////////////Debug.Log("hit");
        animator.SetTrigger("Flinch");
    }

    public override void PlayHitAudio()
    {
        //GetComponent<AudioSource>().PlayOneShot(roar);
        GetComponent<AudioSource>().PlayOneShot(yelp);
    }
    private IEnumerator DestroyDelay(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }

}
