using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{

    public Transform playerHead;
    RaycastHit hit;
    int layerMask;
    float timer = 0;
    float timeLimit = 3;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 8;
    }

    private void FixedUpdate()
    {
        
        if (Physics.Raycast(playerHead.position, playerHead.TransformDirection(Vector3.forward), out hit, 25f, layerMask))
        {
            timer += Time.deltaTime;
            if (timer >= timeLimit)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            timer = 0;
        }
    }
}
