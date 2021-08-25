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
    Wave wave;
    float destroyDelay = 3;
    bool invincible;

    public enum PHASE {
        PHASE1,
        PHASE2,
        PHASE3,
        PHASE4
    }

    private PHASE currentPhase;

    private bool isDying = false;
    void Start()
    {
        //////////anim.Play("birth");
        currentPhase = PHASE.PHASE1;
        this.GetComponent<AudioSource>().PlayOneShot(roar);
        wave = GameObject.Find("Dragonwave").GetComponent<Wave>();
        invincible = false;
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
        if (currentPhase == PHASE.PHASE1 && health < 400) {
            enterPhase2();
        }
        else if (currentPhase == PHASE.PHASE2 && health < 300) {
            enterPhase3();
        }
        else if (currentPhase == PHASE.PHASE3 && health < 200) {
            enterPhase4();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            StartCoroutine(DestroyDelay(destroyDelay));
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            int attackInt = Random.Range(1, 4);
            animator.SetInteger("Attack 0",attackInt);
            //animator.SetTrigger("Attack");
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

    private IEnumerator DragonWave(Wave wave) {
        Vector3 end = transform.position - (transform.forward * 10);
        StartCoroutine(MoveOverSeconds(end, 5f));
        wave.SpawnEnemies();
        invincible = true;
        while (wave.EnemiesLeft() > 0) {
            yield return null;
        }
        invincible = false;
        end = transform.position + (transform.forward * 10);
        StartCoroutine(MoveOverSeconds(end, 5f));
    }

    private void enterPhase2() {
        Debug.Log("Dragon: Entering Phase 2");
        currentPhase = PHASE.PHASE2;
        StartCoroutine(DragonWave(wave));
        Debug.Log("Dragon: Ending Phase 2");
    }

    private void enterPhase3() {
        Debug.Log("Dragon: Entering Phase 3");
        currentPhase = PHASE.PHASE3;
        StartCoroutine(DragonWave(wave));
        Debug.Log("Dragon: Ending Phase 3");
    }

    private void enterPhase4() {
        Debug.Log("Dragon: Entering Phase 4");
        currentPhase = PHASE.PHASE4;
        StartCoroutine(DragonWave(wave));
        Debug.Log("Dragon: Ending Phase 4");
    }
}
