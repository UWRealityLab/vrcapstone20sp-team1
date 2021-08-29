using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeFadeIn : MonoBehaviour
{
    float fullVolume = .85f;
    float fadeTime = 1.5f;
    bool reachedGoal = false;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        while(timer <= fadeTime)
        {
            timer += Time.deltaTime;
            foreach (Transform child in transform)
            {
                child.GetComponent<AudioSource>().volume = timer / fadeTime * fullVolume;
            }
            yield return new WaitForEndOfFrame();
        }
    }


}
