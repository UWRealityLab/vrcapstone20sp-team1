using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light lt;
    public float minIntensity = 0.70f;
    public float maxIntensity = 1.0f;
    GameManager manager;

    float random;

    void Start()
    {
        manager = GameManager.GetInstance();
        lt = GetComponent<Light>();
        random = Random.Range(0.0f, 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetLevel().Equals("final"))
        {
            minIntensity = 0.1f;
            maxIntensity = 0.4f; 
        }
        float noise = Mathf.PerlinNoise(random, Time.time);
        lt.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
