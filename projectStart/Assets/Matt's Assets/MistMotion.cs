using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistMotion : MonoBehaviour
{
    Vector3 currentPosition;
    Vector3 previousPostion;
    ParticleSystem particles;
    bool wasStill = false;
    float stillTimer;
    float timeToRemainStill = .10f;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        previousPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        if (currentPosition != previousPostion && wasStill == true)
        {
            wasStill = false;
            particles.Clear();
            //particles.startLifetime = .5f;
        }
        if (currentPosition == previousPostion)
        {
            stillTimer += Time.deltaTime;
            if (stillTimer >= timeToRemainStill)
            {
                wasStill = true;
                particles.startLifetime = 3.0f;
                //particles.emission.rateOverTime = 8f;
            }
            
        }
        else
        {
            stillTimer = 0;
            particles.startLifetime = .5f;
        }
        previousPostion = transform.position;
    }
}
