using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    public float speed = 0.2f;
    //public final position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = new Vector3(0, 1, 0); //calculate position of the player
        if(transform.position.y < 2)
        {
            Debug.Log(transform.position);
            transform.Translate(v * Time.deltaTime * speed);
        }
    }
}
