using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;
public class SmallDragonScript : Monster
{
    public float speed = 4;
    public float range = 3;
    public NavMeshAgent agent;
    public Animation anim;
    public AudioClip roar;
    public AudioClip death;
    public Player player;
    private GameManager manager;
    private Grandpa grandpa;
    // Start is called before the first frame update
    private bool isDying = false;
    private bool first = true;
    
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
        anim.Play("run");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDying)
        {
                Destroy(this.gameObject);
        }
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            Debug.Log("Dragon stopped");
            agent.SetDestination(transform.position);
            playAttack();
        }
        else
        {
            Debug.Log("dragon run");
            agent.SetDestination(player.transform.position);
            playAnimation("run");

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
        Debug.Log(" dragon hit");
    }

    public override void PlayHitAudio()
    {
        //GetComponent<AudioSource>().PlayOneShot(roar);
    }

    private void playAnimation(string animationName)
    {
        if (!anim.isPlaying)
        {
            anim.Play(animationName);
        }
    }
    private void playAttack()
    {
        if (!anim.isPlaying || first)
        {
            Debug.Log("dragon animation");
            first = false;
            int attackNumber = Random.Range(1, 4);
            anim.Play("attack" + attackNumber);
            if(attackNumber == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(roar);
            }
        }
    }
    void OnDestroy()
    {
        grandpa.onMonsterDeath();
    }
}
