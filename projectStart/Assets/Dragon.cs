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
    Wave wave1;
    float destroyDelay = 3;

    public enum PHASE {
        PHASE1,
        PHASE2,
        PHASE3
    }

    private PHASE currentPhase;

    private bool isDying = false;
    void Start()
    {
        //////////anim.Play("birth");
        currentPhase = PHASE.PHASE1;
        this.GetComponent<AudioSource>().PlayOneShot(roar);
        wave1 = GameObject.Find("Dragonwave1").GetComponent<Wave>();
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
        if (currentPhase == PHASE.PHASE1 && health < 400) {
            enterPhase2();
        }
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

    private IEnumerator MoveOverSeconds (Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds)
        {
            //Debug.Log("Dragon: Moving... T =" + elapsedTime/seconds + ", position = " + transform.position);
            transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return null;//new WaitForEndOfFrame();
        }
        transform.position = end;
    }

    private IEnumerator DragonWave() {
        while (wave1.EnemiesLeft() > 0) {
            yield return null;
        }
        Vector3 end = transform.position + (transform.forward * 10);
        StartCoroutine(MoveOverSeconds(end, 5f));
    }

    private void enterPhase2() {
        Debug.Log("Dragon: Entering Phase 2");
        currentPhase = PHASE.PHASE2;
        Vector3 end = transform.position - (transform.forward * 10);
        StartCoroutine(MoveOverSeconds(end, 5f));
        wave1.SpawnEnemies();

        Debug.Log("Dragon: Ending Phase 2");
    }

}
