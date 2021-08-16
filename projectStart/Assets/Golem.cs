using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Monster
{
    public AudioClip pain;
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
        animator.SetBool("Moving", false);
        animator.SetBool("Die", true);
        StartCoroutine(Dying());
    }
    public override void HitReaction()
    {
        animator.SetTrigger("Hit");
        animator.ResetTrigger("Hit");
    }

    public override void PlayHitAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(pain);
    }
    private IEnumerator Dying()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Finish"))
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
