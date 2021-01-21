using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing2 : MonoBehaviour
{
    float goal = 100f;
    float intensity;
    float timer;
    float timeToGoal = 5f;
    int sign = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= timeToGoal)
        {
            Debug.Log("Intensity: " + intensity + ", Timer: " + timer);
            intensity += sign * goal / timeToGoal * Time.deltaTime;
        }
        else
        {
            timer = 0;
            sign *= -1;
        }
    }
}
