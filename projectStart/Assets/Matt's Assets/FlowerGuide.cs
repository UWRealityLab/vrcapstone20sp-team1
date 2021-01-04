using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGuide : MonoBehaviour
{
    public bool toggleLight;
    //public float blinkDuration;
    public float blinkFrequency;
    public GameObject[] flowers;
    bool guidanceOn = false;
    float timer;
    GameObject activeFlower1;
    GameObject activeFlower2;
    int arrayCounter;
    Component halo;
    

    // Start is called before the first frame update
    void Start()
    {
        StartFlowerGuidance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartGuiding()
    {
        foreach (GameObject f in flowers)
        {
            DeactivateFlower(f);
        }
        activeFlower1 = flowers[0];
        activeFlower2 = flowers[1];
        while(guidanceOn == true)
        {
            timer += Time.deltaTime;
            if (timer >= blinkFrequency)
            {
                DeactivateFlower(activeFlower1);
                DeactivateFlower(activeFlower2);
                GetNextFlowers();
                ActivateFlower(activeFlower1);
                ActivateFlower(activeFlower2);
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
    void ActivateFlower(GameObject f)
    {
        if (toggleLight == true)
        {
            f.GetComponent<Light>().enabled = true;
        }
        //f.GetComponent<Halo>().enabled = false;
        halo = f.GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
    }
    void DeactivateFlower(GameObject f)
    {
        if (toggleLight == true)
        {
            f.GetComponent<Light>().enabled = false;
        }
        //f.GetComponent<Halo>().enabled = false;
        halo = f.GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
    }
    public void StartFlowerGuidance()
    {
        guidanceOn = true;
        StartCoroutine(StartGuiding());
    }
    public void StopFlowerGuidance()
    {
        guidanceOn = false;
        foreach (GameObject f in flowers)
        {
            ActivateFlower(f);
        }
    }
}
