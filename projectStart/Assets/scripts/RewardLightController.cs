using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardLightController : MonoBehaviour
{
    public Light lt;
    public float minIntensity = 3.0f;
    public float maxIntensity = 2.7f;
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
        float noise = Mathf.PerlinNoise(random, Time.time);
        lt.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
