using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

public class Testing : MonoBehaviour
{
    ParticleSystem particle;
    float switchTime = 1f;
    float timer = 0;
    Gradient lit = new Gradient();
    Gradient unlit = new Gradient();
    float lifetime;
    bool on = false;
    

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        lifetime = particle.main.duration;
        unlit.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(176,250,247)/255f, 0.0f), new GradientColorKey(new Color(176, 250, 247)/255, lifetime) }, new GradientAlphaKey[] { new GradientAlphaKey(145f/255f, 0.0f), new GradientAlphaKey(0.0f, lifetime) });
        lit.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(221, 250, 249)/255f, 0.0f), new GradientColorKey(new Color(221, 250, 249)/255, lifetime) }, new GradientAlphaKey[] { new GradientAlphaKey(145f / 255f, 0.0f), new GradientAlphaKey(0.0f, lifetime) });
        switchTime = Random.Range(0, .2f);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= switchTime)
        {
            timer = 0;
            switchTime = Random.Range(0, .2f);
            if (on) 
            {
                Debug.Log("Off");
                on = false;
                var col = particle.colorOverLifetime;
                col.color = unlit;
            }
            else
            {
                Debug.Log("On");
                on = true;
                var col = particle.colorOverLifetime;
                col.color = lit;
            }
        }
    }




}
