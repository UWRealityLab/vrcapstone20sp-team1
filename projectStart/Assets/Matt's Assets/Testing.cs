using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    float timer;
    Component halo;

    // Start is called before the first frame update
    void Start()
    {
        halo = GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
        }
    }
}
