using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndParticleTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gemParticles;
    public GameObject endParticles;

    public void Transition()
    {
        foreach (Transform child in gemParticles.transform)
        {
            if (child.GetComponent<ParticleSystem>() != null)
            {
                child.GetComponent<ParticleSystem>().Stop();
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in endParticles.transform)
        {
            if (child.GetComponent<ParticleSystem>() != null)
            {
                child.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }


}
