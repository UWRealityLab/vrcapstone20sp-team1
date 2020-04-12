using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emit : MonoBehaviour
{
    bool hasParticles;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        hasParticles = false;
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Stop();
    }

    private void OnWillRenderObject()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
