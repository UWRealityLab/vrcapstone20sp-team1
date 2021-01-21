using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerFade : MonoBehaviour
{
    float haloIntensity;
    float lightIntensity;
    Light light;
    Light halo;
    // Start is called before the first frame update
    void Start()
    {
        light =GetComponent<Light>();
        halo = transform.Find("Halo").GetComponent<Light>();
    }

    public void FadeIn(float maxHalo, float maxLight, float fadeTime)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOn(maxHalo, maxLight, fadeTime));
    }
    public void FadeOut(float maxHalo, float maxLight, float fadeTime)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOff(maxHalo, maxLight, fadeTime));
    }
    public void Off()
    {
        StopAllCoroutines();
        light.intensity = 0;
        halo.intensity = 0;
    }
    public void On(float maxHalo, float maxLight)
    {
        StopAllCoroutines();
        light.intensity = maxLight;
        halo.intensity = maxHalo;
    }

    IEnumerator FadeOn(float maxHalo, float maxLight, float fadeTime)
    {
        while (haloIntensity < maxHalo)
        {
            haloIntensity += maxHalo / fadeTime * Time.deltaTime;
            lightIntensity += maxLight / fadeTime * Time.deltaTime;
            light.intensity = lightIntensity;
            halo.intensity = haloIntensity;
            yield return new WaitForEndOfFrame();
        }
        FadeOut(maxHalo, maxLight, fadeTime);
    }
    IEnumerator FadeOff(float maxHalo, float maxLight, float fadeTime)
    {
        while (haloIntensity > 0)
        {
            haloIntensity -= maxHalo / fadeTime * Time.deltaTime;
            lightIntensity -= maxLight / fadeTime * Time.deltaTime;
            light.intensity = lightIntensity;
            halo.intensity = haloIntensity;
            yield return new WaitForEndOfFrame();
        }
    }
}
