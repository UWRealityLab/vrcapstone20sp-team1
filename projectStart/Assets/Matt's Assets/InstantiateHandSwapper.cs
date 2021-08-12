﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InstantiateHandSwapper : MonoBehaviour
{
    public Hand hand;
    public GameObject swapper;
    int childCount;

    // Start is called before the first frame update
    void Start()
    {
        childCount = transform.childCount;
        StartCoroutine(WaitForEnable());

    }
    IEnumerator WaitForEnable()
    {
        while (transform.childCount == childCount)
        {
            yield return new WaitForEndOfFrame();
        }
        if (swapper != null && swapper.activeInHierarchy == true)
        {
            swapper.SetActive(true);
            swapper.transform.SetParent(hand.transform);
            StopCoroutine(WaitForEnable());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        swapper.SetActive(true);
        swapper.transform.SetParent(hand.transform);
    }
}