using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;
public class SmallDragonScript : Monster
{
    public float range = 3;
    public NavMeshAgent agent;
    public AudioClip roar;
    public AudioClip death;
    public Player player;
    private GameManager manager;
    private Grandpa grandpa;
    // Start is called before the first frame update
    private bool isDying = false;
    private bool first = true;
    float destroyDelay = 3;
    public bool mid = false;

    void Start()
    {
        int diff = Random.Range(213, 333);
        Random.seed = diff;
        manager = GameManager.GetInstance();
        grandpa = Grandpa.GetInstance();
        player = Player.instance;

        if (agent == null || manager == null)
        {
            Debug.Log("something is wrong");
        }
        GetComponent<AudioSource>().PlayOneShot(roar);
        animator.SetBool("Run", true);
        //animator.SetTrigger("Run2");
    }

    // Update is called once per frame
    void Update()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            StartCoroutine(DestroyDelay(destroyDelay));
            agent.SetDestination(transform.position);
        }
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            Debug.Log("Dragon stopped");
            agent.SetDestination(transform.position);
            animator.SetBool("Run", false);
            playAttack();
        } else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            agent.SetDestination(transform.position);
        } else
        {
            Debug.Log("dragon run");
            agent.SetDestination(player.transform.position);
            animator.SetBool("Run", true);
           /* if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.SetTrigger("Run2");
            }*/
        }
    }
    public override void Death()
    {
        GetComponent<AudioSource>().PlayOneShot(death);
        animator.SetBool("Run", false);
        animator.SetBool("Dying", true);
    }
    public override void HitReaction()
    {
        Debug.Log(" dragon hit");
        animator.SetTrigger("Flinch");
    }

    public override void PlayHitAudio()
    {
        //GetComponent<AudioSource>().PlayOneShot(roar);
    }

    private void playAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Debug.Log("dragon animation");
            int attackNumber = Random.Range(1, 4);
            string attackS = "Attack" + attackNumber;
            Debug.Log(attackS);
            if(mid)
            {
                animator.SetTrigger("Attack3");
            }
            else
            {
                animator.SetTrigger("Attack1");
            }
        }
    }

    private IEnumerator DestroyDelay(float t)
    {
        yield return new WaitForSeconds(t);
        grandpa.onMonsterDeath();
        Destroy(gameObject);
    }

}
