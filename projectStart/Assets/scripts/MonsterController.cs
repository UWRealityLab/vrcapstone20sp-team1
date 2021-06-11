using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class MonsterController : MonoBehaviour
{
    public float speed = 4;
    public float range = 1.5f;
    public NavMeshAgent agent;
    public Player player;
    private GameManager manager;
    private Grandpa grandpa;
    public Animator anim;
    public float attackDelay = 2.0f;
    bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.GetInstance();
        grandpa = Grandpa.GetInstance();
        player = Player.instance;
        if (agent == null || manager == null)
        {
            Debug.Log("something is wrong");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < range)
        {
            //Debug.Log("stopped");
            agent.SetDestination(transform.position);
            anim.SetBool("Moving", false);
            if (attacking == false)
            {
                attacking = true;
                StartCoroutine(Attacking());
            }
        }
        else
        {               
            agent.SetDestination(player.transform.position);
            anim.SetBool("Moving", true);
        }        
    }
    IEnumerator Attacking()
    {
        
        while (attacking == true)
        {
            anim.SetTrigger("Attack");
            anim.ResetTrigger("Attack");
            yield return new WaitForSeconds(attackDelay);
        }
    }
    void OnDestroy()
    {
        grandpa.onMonsterDeath();
    }
}
