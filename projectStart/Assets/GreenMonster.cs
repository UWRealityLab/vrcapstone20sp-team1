using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class GreenMonster : Monster
{
    public AudioClip pain;
    public NavMeshAgent agent;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            Debug.Log("Not Run");
            agent.SetDestination(transform.position);
            Debug.Log(agent.gameObject.name);
            animator.SetBool("Moving", false);
        }
        else
        {
            agent.SetDestination(player.transform.position);
            animator.SetBool("Moving", true);
           
        }
            */
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
        //animator.ResetTrigger("Hit");
    }

    public override void PlayHitAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(pain);
    }
    private IEnumerator Dying()
    {
        /*
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Finish"))
        {
            yield return new WaitForEndOfFrame();
        }
        */
        yield return new WaitForSeconds(2.75f);
        Destroy(gameObject);
    }
}
