using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShrineAttention : MonoBehaviour
{
    float maxLightIntensity;
    float maxHaloIntensity;
    public float blinkFrequency;
    public float fadeTime = 1;
    public GameObject[] flowers;
    public Light emerald;
    public float lightRange = 10.0f;
    float originalRange;
    bool guidanceOn = false;
    float timer;
    GameObject activeFlower1;
    GameObject activeFlower2;
    int arrayCounter;
    GameManager manager;
    private bool started = false;




    // Start is called before the first frame update
    void Start()
    {
        //fadeTime = blinkFrequency;
        manager = GameManager.GetInstance();
        maxLightIntensity = flowers[0].GetComponent<Light>().intensity;
        maxHaloIntensity = flowers[0].transform.Find("Halo").GetComponent<Light>().intensity;
        originalRange = emerald.range;

    }

    IEnumerator StartGuiding()
    {
        
        foreach (GameObject f in flowers)
        {
            f.GetComponent<FlowerFade>().Off();
            
        }

        activeFlower1 = flowers[0];
        activeFlower2 = flowers[1];
        activeFlower1.GetComponent<FlowerFade>().FadeIn(maxHaloIntensity, maxLightIntensity, fadeTime);
        activeFlower2.GetComponent<FlowerFade>().FadeIn(maxHaloIntensity, maxLightIntensity, fadeTime);
        while (guidanceOn == true)
        {
            timer += Time.deltaTime;
            if (timer >= blinkFrequency)
            {

               // activeFlower1.GetComponent<FlowerFade>().FadeOut(maxHaloIntensity, maxLightIntensity, fadeTime);
                //activeFlower2.GetComponent<FlowerFade>().FadeOut(maxHaloIntensity, maxLightIntensity, fadeTime);
                GetNextFlowers();
                activeFlower1.GetComponent<FlowerFade>().FadeIn(maxHaloIntensity, maxLightIntensity, fadeTime);
                activeFlower2.GetComponent<FlowerFade>().FadeIn(maxHaloIntensity, maxLightIntensity, fadeTime);

                timer = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    void GetNextFlowers()
    {
        if(arrayCounter + 2 >= flowers.Length)
        {
            arrayCounter = 0;
        }
        else
        {
            arrayCounter += 2;
        }
        activeFlower1 = flowers[arrayCounter];
        activeFlower2 = flowers[arrayCounter + 1];
    }

    public void StartGuidance()
    {
        guidanceOn = true;
        emerald.range = lightRange;
        StartCoroutine(StartGuiding());
    }
    public void StopGuidance()
    {
        guidanceOn = false;
        emerald.range = originalRange;
        foreach (GameObject f in flowers)
        {
            f.GetComponent<FlowerFade>().On(maxHaloIntensity, maxLightIntensity);
        }
    }

    void Update()
    {
        if(manager.GetLevel() == GameManager.LEVEL.DRAGON_BOSS && manager.InProgress() && !started)
        {
            started = true;
            StartGuidance();
        }
    }
}
