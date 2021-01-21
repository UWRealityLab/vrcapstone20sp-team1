using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    float maxLightIntensity;
    float maxHaloIntensity;
    float hIntensity;
    float lIntensity;
    float transitionTime = 1;
    float timer = 0;
    public bool turningOn;
    int sign;

    // Start is called before the first frame update
    void Awake()
    {
        maxLightIntensity =gameObject.GetComponent<Light>().intensity;
        Debug.Log(maxLightIntensity);
        maxHaloIntensity = gameObject.transform.Find("Halo").GetComponent<Light>().intensity;

        if (turningOn == true)
        {
            sign = 1;
            gameObject.GetComponent<Light>().intensity = 0;
            gameObject.transform.Find("Halo").GetComponent<Light>().intensity = 0;
            hIntensity = 0;
            lIntensity = 0;
            Debug.Log("Starting Intensity: " + gameObject.GetComponent<Light>().intensity);
        }
        else
        {
            sign = -1;
            gameObject.GetComponent<Light>().intensity = maxLightIntensity;
            gameObject.transform.Find("Halo").GetComponent<Light>().intensity = maxHaloIntensity;
            hIntensity = maxHaloIntensity;
            lIntensity = maxLightIntensity;
        }
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        /*
        if (timer >= transitionTime)
        {
            lIntensity += sign * maxLightIntensity / transitionTime * Time.deltaTime;
            hIntensity += sign *maxHaloIntensity / transitionTime * Time.deltaTime;
            gameObject.GetComponent<Light>().intensity = lIntensity;
            gameObject.transform.Find("Halo").GetComponent<Light>().intensity = hIntensity;
        }
        else
        {
            sign *= -1;
            timer = 0;
        }
        */
        if (timer >= transitionTime)
        {
            turningOn = !turningOn;
            StopAllCoroutines();

            if (turningOn== true)
            {
                StartCoroutine(FadeIn());
            }
            else
            {

                StartCoroutine(FadeOut());
            }
            timer = 0;
        }

    }
    IEnumerator FadeIn()
    {
        //hIntensity = 0;
        //lIntensity = 0;
        while (gameObject.GetComponent<Light>().intensity <= maxLightIntensity)
        {
            lIntensity += maxLightIntensity / transitionTime * Time.deltaTime;
            hIntensity += maxHaloIntensity / transitionTime * Time.deltaTime;
            gameObject.GetComponent<Light>().intensity = lIntensity;
            gameObject.transform.Find("Halo").GetComponent<Light>().intensity = hIntensity;

            yield return new WaitForEndOfFrame();
        }

    }
    IEnumerator FadeOut()
    {
        //hIntensity = maxHaloIntensity;
        //lIntensity = maxLightIntensity;
        while (gameObject.GetComponent<Light>().intensity >= 0)
        {
            lIntensity -= maxLightIntensity / transitionTime * Time.deltaTime;
            hIntensity -= maxHaloIntensity / transitionTime * Time.deltaTime;
            gameObject.GetComponent<Light>().intensity = lIntensity;
            gameObject.transform.Find("Halo").GetComponent<Light>().intensity = hIntensity;

            yield return new WaitForEndOfFrame();
        }
    }
}
