using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public float speed = 4;
    public int wave = 1;
    public float range = 2;
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject player;
    GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.GetInstance();
        if(agent == null || manager == null)
        {
            Debug.Log("something is wrong");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetAttackWave() >= wave && manager.GetLevel().Equals("fightMonsters"))
        {
            if(Vector3.Distance(transform.position, player.transform.position) < range)
            {
                //Debug.Log("stopped");
                agent.SetDestination(transform.position);
            }
            else
            {
                //Debug.Log("chasing");
                agent.SetDestination(player.transform.position);
            }

            //follow the Player!!
        }        
        
    }
    void onDestroy()
    {
        manager.IncrementDead();
    }
}
