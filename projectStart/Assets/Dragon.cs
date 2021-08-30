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
    Wave wave2;
    float destroyDelay = 3;
    Vector3 prevFramePos;
    bool invincible;
    bool wounded;
    public FireBreath fireBreath;

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
        wave2 = GameObject.Find("Dragonwave2").GetComponent<Wave>();
        invincible = false;
        wounded = false;
    }

    public void OnTriggerEnter(Collider collider) {
        if (!invincible) {
            base.OnTriggerEnter(collider);
        }
    }
    public void OnCollisionEnter(Collision collision) {
        if (!invincible) {
            base.OnCollisionEnter(collision);
        }
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
        if (currentPhase == PHASE.PHASE1 && health < 175) {
            enterPhase2();
        }
        else if (currentPhase == PHASE.PHASE2 && health < 100) {
            enterPhase3();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            StartCoroutine(DestroyDelay(destroyDelay));
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !wounded)
        {
            int attackInt = Random.Range(1, 4);
            //animator.SetInteger("Attack 0", 3);
            animator.SetInteger("Attack 0", attackInt);
            //animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetInteger("Attack 0", 0);
        }
        float movementPerFrame = Vector3.Distance(prevFramePos, transform.position);
        float speed = movementPerFrame / Time.deltaTime;
        animator.SetFloat("Speed", speed);
        prevFramePos = transform.position;
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
        //fireBreath.AbruptStop();
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
            fireBreath.AbruptStop();
            transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return null;//new WaitForEndOfFrame();
        }
        transform.position = end;
    }

    private IEnumerator DragonWave(Wave wave) {
        Vector3 end = transform.position - (transform.forward * 10);
        fireBreath.AbruptStop();
        animator.SetBool("isReversing", true);
        wounded = true;
        StartCoroutine(MoveOverSeconds(end, 5f));
        wave.SpawnEnemies();
        invincible = true;
        while (wave.EnemiesLeft() > 0) {
            yield return null;
        }
        invincible = false;
        end = transform.position + (transform.forward * 10);
        animator.SetBool("isReversing", false);
        wounded = false;
        StartCoroutine(MoveOverSeconds(end, 5f));
    }

    private void enterPhase2() {
        Debug.Log("Dragon: Entering Phase 2");
        currentPhase = PHASE.PHASE2;
        StartCoroutine(DragonWave(wave1));
        Debug.Log("Dragon: Ending Phase 2");
    }

    private void enterPhase3() {
        Debug.Log("Dragon: Entering Phase 3");
        currentPhase = PHASE.PHASE3;
        StartCoroutine(DragonWave(wave2));
        Debug.Log("Dragon: Ending Phase 3");
    }
}
