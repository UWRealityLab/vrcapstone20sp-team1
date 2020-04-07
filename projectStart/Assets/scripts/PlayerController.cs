using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed = 4;
    public Camera cam;
    public NavMeshAgent agent;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.GetInstance();
        if (agent == null || manager == null)
        {
            Debug.Log("something is wrong");
        }
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }*/
    }
}
