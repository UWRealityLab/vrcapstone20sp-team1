using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispAnimation : MonoBehaviour
{

    public float floatStrength = 1;
    public float floatSpeed = 1;

    private float sinedValue;
    private Vector3 prevAnchorPos;

    void Start()
    {
        sinedValue = Time.time;
        prevAnchorPos = GetComponentInParent<Transform>().position;
    }

    void Update()
    {
        Vector3 deltaPos = (GetComponentInParent<Transform>().position - prevAnchorPos);
        float parentSpeed = Mathf.Sqrt(deltaPos.x * deltaPos.x + deltaPos.z * deltaPos.z);
        sinedValue += Time.deltaTime * (2 + (parentSpeed * 500));

        transform.position = new Vector3(
            transform.position.x,
            GetComponentInParent<Transform>().position.y + ((float)Mathf.Sin(sinedValue) * (floatStrength * 0.008f)),
            transform.position.z);


        prevAnchorPos = GetComponentInParent<Transform>().position;
    }
}
